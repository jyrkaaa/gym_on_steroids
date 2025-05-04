using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class SetInExercUOWMapper : IMapper<App.DAL.DTO.SetInExerc, App.Domain.EF.SetInExerc>
{
    public SetInExerc? Map(Domain.EF.SetInExerc? entity)
    {
        if (entity == null) return null;
        return new SetInExerc()
        {
            Id = entity.Id,
            ExerInWorkoutId = entity.ExerInWorkoutId,
            Weight = entity.Weight,
            ExerInWorkout = new ExerInWorkout()
            {
                Id = entity.ExerInWorkoutId,
                ExerciseId = entity.ExerInWorkoutId,
            }
        };
    }

    public Domain.EF.SetInExerc? Map(SetInExerc? entity)
    {
        if (entity == null) return null;
        return new Domain.EF.SetInExerc()
        {
            Id = entity.Id,
            ExerInWorkoutId = entity.ExerInWorkoutId,
            Weight = entity.Weight,
            ExerInWorkout = new Domain.EF.ExerInWorkout()
            {
                Id = entity.ExerInWorkoutId,
                ExerciseId = entity.ExerInWorkoutId,
            }
        };
    }
}