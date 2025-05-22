import { BaseEntityService } from './BaseEntityService.ts'
import type { IExercise } from '@/domain/IExercise.ts'
import type { IResultObject } from '@/types.ts'
import { BaseService } from '@/services/BaseService.ts'
import type { IExerciseDelete } from '@/domain/IExerciseDelete.ts'

export class ExerciseService extends BaseEntityService<IExercise> {
  constructor() {
    super('exercise')
  }
  async removeAsync2(id : string): Promise<IResultObject<IExerciseDelete>> {
    try {
      let options = {}
      if (this.store.isAuthenticated) {
        options = {
          headers: {
            Authorization: `Bearer ${this.store.jwt}`,
          },
        }
      }
      const response = await BaseService.axios.delete<IExerciseDelete>(this.basePath + "/" + id, options)

      //console.log('getAll response', response)

      if (response.status <= 300) {
        return { data: response.data }
      }

      return {
        errors: [(response.status.toString() + ' ' + response.statusText).trim()],
      }
    } catch (error) {
      console.log('error: ', (error as Error).message)
      return {
        errors: [JSON.stringify(error)],
      }
    }
  }
}
