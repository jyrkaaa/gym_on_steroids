import type { IDomainId } from './IDomainId'
import type { IExercise } from '@/domain/IExercise.ts'

export interface IUsersInWorkout extends IDomainId {
  netUserId: string
  workoutId: string
}
