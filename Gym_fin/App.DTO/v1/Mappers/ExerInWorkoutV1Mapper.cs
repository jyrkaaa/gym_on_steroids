using Base.BLL.Contracts;

namespace App.DTO.v1.Mappers;

public class ExerInWorkoutV1Mapper : IBLLMapper<App.DTO.v1.ExerInWorkout, App.BLL.DTO.ExerInWorkout>
{
    public ExerInWorkout? Map(BLL.DTO.ExerInWorkout? entity)
    {
        if (entity == null) return null;
        return new ExerInWorkout()
        {
            Id = entity.Id,
            Desc = entity.Desc,
            WorkoutId = entity.WorkoutId,
            ExerciseId = entity.ExerciseId,
            Exercise = new Exercise()
            {
                Id = entity.ExerciseId,
                Name = entity.Exercise!.Name,
                ExerciseCategoryId = entity.Exercise.ExerciseCategoryId,
            },
            Workout = new Workout()
            {
                Id = entity.WorkoutId,
                Name = entity.Workout!.Name,
                Users = null,
                Public = entity.Workout!.Public,
                Date = entity.Workout!.Date,
            }
        };
    }

    public BLL.DTO.ExerInWorkout? Map(ExerInWorkout? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.ExerInWorkout()
        {
            Id = entity.Id,
            Desc = entity.Desc,
            WorkoutId = entity.WorkoutId,
            ExerciseId = entity.ExerciseId,
            Exercise = new BLL.DTO.Exercise()
            {
                Id = entity.ExerciseId,
                Name = entity.Exercise!.Name,
                ExerciseCategoryId = entity.Exercise.ExerciseCategoryId,
            },
            Workout = new BLL.DTO.Workout()
            {
                Id = entity.WorkoutId,
                Name = entity.Workout!.Name,
                Users = null,
                Public = entity.Workout!.Public,
                Date = entity.Workout!.Date,
            }
        };
    }
}