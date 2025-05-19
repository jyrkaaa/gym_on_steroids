import type { IDomainId } from './IDomainId'
import type { IExercise } from '@/domain/IExercise.ts'

export interface IUserWeight extends IDomainId {
  weightKg: number
  desc: string | null
  date: string
  netUserId: string | null
}
