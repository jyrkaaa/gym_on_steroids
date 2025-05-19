import type { IDomainId } from './IDomainId'
import type { IExercise } from '@/domain/IExercise.ts'

export interface IExerciseCategory extends IDomainId {
  name: string
  exercises: IExercise[]
}
