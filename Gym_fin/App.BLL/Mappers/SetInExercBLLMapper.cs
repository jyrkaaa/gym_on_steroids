using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class SetInExercBLLMapper : IMapper<App.BLL.DTO.SetInExerc, App.DAL.DTO.SetInExerc>
{
    public SetInExerc? Map(DTO.SetInExerc? entity)
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

    public DTO.SetInExerc? Map(SetInExerc? entity)
    {
        if (entity == null) return null;
        return new DTO.SetInExerc()
        {
            Id = entity.Id,
            ExerInWorkoutId = entity.ExerInWorkoutId,
            Weight = entity.Weight,
            ExerInWorkout = new DTO.ExerInWorkout()
            {
                Id = entity.ExerInWorkoutId,
                ExerciseId = entity.ExerInWorkoutId,
            }
        };
    }
}