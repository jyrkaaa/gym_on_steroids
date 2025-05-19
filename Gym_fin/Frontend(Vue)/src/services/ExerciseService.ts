import { BaseEntityService } from './BaseEntityService.ts'
import type { IExercise } from '@/domain/IExercise.ts'

export class ExerciseService extends BaseEntityService<IExercise> {
  constructor() {
    super('exercise')
  }
}
