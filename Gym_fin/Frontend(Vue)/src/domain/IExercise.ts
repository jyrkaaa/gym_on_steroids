import type { IDomainId } from './IDomainId'

export interface IExercise extends IDomainId {
  name: string
  desc: string | null
  date: string
  exerTargetId: string | null
  exerGuideId: string | null
  exerciseCategoryId: string
}
