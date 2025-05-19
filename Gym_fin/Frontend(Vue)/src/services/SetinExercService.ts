import { BaseEntityService } from './BaseEntityService.ts'
import type { IWorkout } from '@/domain/IWorkout.ts'
import type { ISetInExerc } from '@/domain/ISetInExerc.ts'
import type { IResultObject } from '@/types.ts'
import { BaseService } from '@/services/BaseService.ts'

export class SetinExercService extends BaseEntityService<ISetInExerc> {
  constructor() {
    super('SetInExerc')
  }
  async getBestSet(id: string): Promise<IResultObject<ISetInExerc[]>> {
    try {
      let options = {}
      if (this.store.isAuthenticated) {
        options = {
          headers: {
            Authorization: `Bearer ${this.store.jwt}`,
          },
        }
      }
      const response = await BaseService.axios.get<ISetInExerc[]>(this.basePath + "/max/" + id, options)

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
