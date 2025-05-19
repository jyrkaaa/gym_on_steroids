import type { IDomainId } from '@/domain/IDomainId.ts'
import type { IExerInWorkout } from '@/domain/IExerInWorkout.ts'
import type { IUser } from '@/domain/IUser.ts'

export interface IWorkout extends IDomainId {
  date: string
  public: boolean
  name: string
  exercises: IExerInWorkout[] | null
  users: IUser[] | null
}
