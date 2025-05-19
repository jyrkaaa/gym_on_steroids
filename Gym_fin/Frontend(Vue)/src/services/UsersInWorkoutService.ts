import { BaseEntityService } from './BaseEntityService.ts'
import type { IWorkout } from '@/domain/IWorkout.ts'
import type { IResultObject } from '@/types.ts'
import { BaseService } from '@/services/BaseService.ts'
import type { IUsersInWorkout } from '@/domain/IUsersInWorkout.ts'

export class UsersInWorkoutService extends BaseEntityService<IUsersInWorkout> {
  constructor() {
    super('UsersInWorkout')
  }
}
