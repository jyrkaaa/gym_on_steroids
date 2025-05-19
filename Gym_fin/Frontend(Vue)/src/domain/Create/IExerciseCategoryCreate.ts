
export interface IExerciseCreate {
  name: string
  desc: string | null
  date: string
  exerTargetId: string | null
  exerGuideId: string | null
  exerciseCategoryId: string
}
