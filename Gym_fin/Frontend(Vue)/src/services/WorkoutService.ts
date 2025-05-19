import { BaseEntityService } from './BaseEntityService.ts'
import type { IWorkout } from '@/domain/IWorkout.ts'
import type { IResultObject } from '@/types.ts'
import { BaseService } from '@/services/BaseService.ts'

export class WorkoutService extends BaseEntityService<IWorkout> {
  constructor() {
    super('Workout')
  }
  async getAllAsyncModified(name: string | undefined, dateFrom: string |undefined, dateTo: string | undefined): Promise<IResultObject<IWorkout[]>> {
    try {

      const params = new URLSearchParams();

      if (name) params.append('name', name);
      if (dateFrom) params.append('fromDate', dateFrom); // Use correct parameter names as per API
      if (dateTo) params.append('toDate', dateTo);
      let options = {}
      if (this.store.isAuthenticated) {
        options = {
          headers: {
            Authorization: `Bearer ${this.store.jwt}`,
          },
          params
        }
      }

      const response = await BaseService.axios.get<IWorkout[]>(this.basePath + "/all", options)

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
