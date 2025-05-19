import { BaseEntityService } from './BaseEntityService.ts'
import type { IExerciseCategory } from '@/domain/IExerciseCategory.ts'

export class ExerciseCategoryService extends BaseEntityService<IExerciseCategory> {
  constructor() {
    super('ExerciseCategory')
  }
}
