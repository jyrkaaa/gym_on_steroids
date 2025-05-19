import type { IResultObject } from '@/types.ts'
import { BaseService} from '@/services/BaseService.ts'
import { useAuthStore } from '@/stores/auth.ts'

export abstract class BaseEntityService<TEntity> extends BaseService {
  protected store = useAuthStore()

  protected constructor(protected basePath: string) {
    super()
  }

  async getAllAsync(): Promise<IResultObject<TEntity[]>> {
    try {
      let options = {}
      if (this.store.isAuthenticated) {
        options = {
          headers: {
            Authorization: `Bearer ${this.store.jwt}`,
          },
        }
      }
      const response = await BaseService.axios.get<TEntity[]>(this.basePath + "/all", options)

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

  async addAsync(entity: TEntity): Promise<IResultObject<TEntity>> {
    try {
      let options = {}
      if (this.store.isAuthenticated) {
        options = {
          headers: {
            Authorization: `Bearer ${this.store.jwt}`,
          },
        }
      }
      const response = await BaseService.axios.post<TEntity>(this.basePath, entity, options)

      console.log('login response', response)

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
  async findAsync(id : string): Promise<IResultObject<TEntity[]>> {
    try {
      let options = {}
      if (this.store.isAuthenticated) {
        options = {
          headers: {
            Authorization: `Bearer ${this.store.jwt}`,
          },
        }
      }
      const response = await BaseService.axios.get<TEntity[]>(this.basePath + "/" + id, options)

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
  async removeAsync(id : string): Promise<IResultObject<TEntity[]>> {
    try {
      let options = {}
      if (this.store.isAuthenticated) {
        options = {
          headers: {
            Authorization: `Bearer ${this.store.jwt}`,
          },
        }
      }
      const response = await BaseService.axios.delete<TEntity[]>(this.basePath + "/" + id, options)

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
  async updateAsync(id: string, entity: TEntity): Promise<IResultObject<TEntity>> {
    try {
      let options = {}
      if (this.store.isAuthenticated) {
        options = {
          headers: {
            Authorization: `Bearer ${this.store.jwt}`,
          },
        }
      }
      const response = await BaseService.axios.patch<TEntity>(this.basePath + "/" + id, entity, options)

      console.log('update response', response)

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

