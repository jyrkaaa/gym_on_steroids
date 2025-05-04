using Base.BLL.Contracts;

namespace App.DTO.v1.Mappers;

public class SetInExercV1Mapper : IBLLMapper<App.DTO.v1.SetInExerc, App.BLL.DTO.SetInExerc>
{
    public SetInExerc? Map(BLL.DTO.SetInExerc? entity)
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

    public BLL.DTO.SetInExerc? Map(SetInExerc? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.SetInExerc()
        {
            Id = entity.Id,
            ExerInWorkoutId = entity.ExerInWorkoutId,
            Weight = entity.Weight,
            ExerInWorkout = new BLL.DTO.ExerInWorkout()
            {
                Id = entity.ExerInWorkoutId,
                ExerciseId = entity.ExerInWorkoutId,
            }
        };
    }
}