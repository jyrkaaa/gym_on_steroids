import { BaseEntityService } from './BaseEntityService.ts'
import type { IExerInWorkout } from '@/domain/IExerInWorkout.ts'

export class ExerInWorkoutService extends BaseEntityService<IExerInWorkout> {
  constructor() {
    super('ExerInWorkout')
  }
}
