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
            Reps = entity.Reps,
            ExerInWorkoutId = entity.ExerInWorkoutId,
            Weight = entity.Weight,
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
            Reps = entity.Reps,
            ExerInWorkout = entity.ExerInWorkout != null
                ? new DTO.ExerInWorkout()
                {
                    Id = entity.ExerInWorkoutId,
                    ExerciseId = entity.ExerInWorkout.ExerciseId,
                    WorkoutId = entity.ExerInWorkout.WorkoutId,
                    Workout = entity.ExerInWorkout.Workout != null
                        ? new DTO.Workout()
                        {
                            Id = entity.ExerInWorkout.WorkoutId,
                            Name = entity.ExerInWorkout.Workout?.Name!,
                            Date = entity.ExerInWorkout.Workout?.Date,
                            Public = entity.ExerInWorkout.Workout!.Public!,
                            Users = entity.ExerInWorkout.Workout!.Users?.Select(s => new DTO.UsersInWorkout()
                            {
                                Id = s.Id,
                                WorkoutId = s.WorkoutId,
                                NetUserId = s.NetUserId,
                            }).ToList(),
                        }
                        : null
                }
                : null
        };
    }
}