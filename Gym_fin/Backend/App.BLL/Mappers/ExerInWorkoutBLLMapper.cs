using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class ExerInWorkoutBLLMapper : IMapper<App.BLL.DTO.ExerInWorkout, App.DAL.DTO.ExerInWorkout>
{
    public ExerInWorkout? Map(DTO.ExerInWorkout? entity)
    {
        if (entity == null) return null;
        return new ExerInWorkout()
        {
            Id = entity.Id,
            Desc = entity.Desc,
            WorkoutId = entity.WorkoutId,
            ExerciseId = entity.ExerciseId,
            Exercise = null,
            Workout = null,
        };
    }

    public DTO.ExerInWorkout? Map(ExerInWorkout? entity)
    {
        if (entity == null) return null;
        return new DTO.ExerInWorkout()
        {
            Id = entity.Id,
            Desc = entity.Desc,
            WorkoutId = entity.WorkoutId,
            ExerciseId = entity.ExerciseId,
            Exercise = new DTO.Exercise()
            {
                Id = entity.ExerciseId,
                Name = entity.Exercise!.Name,
                ExerciseCategoryId = entity.Exercise.ExerciseCategoryId,
            },
            Workout = new DTO.Workout()
            {
                Id = entity.WorkoutId,
                Name = entity.Workout!.Name,
                Users = null,
                Public = entity.Workout!.Public,
                Date = entity.Workout!.Date,
            },
            // Sets = entity.Sets?.Select(x => new DTO.SetInExerc()
            // {
            //     Id = x.Id,
            //     Weight = x.Weight,
            //     ExerInWorkoutId = x.ExerInWorkoutId,
            // }).ToList(),
        };
    }
}