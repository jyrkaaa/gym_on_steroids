import type { IDomainId } from '@/domain/IDomainId.ts'
import type { IExercise } from '@/domain/IExercise.ts'
import type { ISetInExerc } from '@/domain/ISetInExerc.ts'

export interface IExerInWorkout extends IDomainId {
  desc: string | null
  workoutId: string | undefined
  exerciseId: string | undefined
  exercise: IExercise | undefined
  Sets: ISetInExerc[] | undefined
}
