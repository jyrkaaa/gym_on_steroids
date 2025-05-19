import { BaseEntityService } from './BaseEntityService.ts'
import type { IUserWeight } from '@/domain/IUserWeight'

export class UserWeightService extends BaseEntityService<IUserWeight> {
  constructor() {
    super('UserWeight')
  }
}
