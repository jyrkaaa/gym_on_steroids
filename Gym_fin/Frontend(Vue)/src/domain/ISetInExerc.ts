import type { IDomainId } from './IDomainId'

export interface ISetInExerc extends IDomainId {
  weight: number
  reps: number
  exerInWorkoutId: string | null
}
