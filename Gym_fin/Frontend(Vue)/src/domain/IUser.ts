import type { IDomainId } from '@/domain/IDomainId.ts'
import type { IExercise } from '@/domain/IExercise.ts'
import type { ISetInExerc } from '@/domain/ISetInExerc.ts'

export interface IUser extends IDomainId {
  netUserId: string
  workoutId: string
}
