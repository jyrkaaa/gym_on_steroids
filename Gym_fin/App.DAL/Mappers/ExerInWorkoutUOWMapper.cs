using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class ExerInWorkoutUOWMapper : IMapper<App.DAL.DTO.ExerInWorkout, App.Domain.EF.ExerInWorkout>
{
    public ExerInWorkout? Map(Domain.EF.ExerInWorkout? entity)
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

    public Domain.EF.ExerInWorkout? Map(ExerInWorkout? entity)
    {
        if (entity == null) return null;
        return new Domain.EF.ExerInWorkout()
        {
            Id = entity.Id,
            Desc = entity.Desc,
            WorkoutId = entity.WorkoutId,
            ExerciseId = entity.ExerciseId,
            Exercise = new Domain.EF.Exercise()
            {
                Id = entity.ExerciseId,
                Name = entity.Exercise!.Name,
                ExerciseCategoryId = entity.Exercise.ExerciseCategoryId,
            },
            Workout = new Domain.EF.Workout()
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