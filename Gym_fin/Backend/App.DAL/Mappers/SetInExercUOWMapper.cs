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
            Reps = entity.Reps,
            ExerInWorkout = entity.ExerInWorkout != null
                ? new ExerInWorkout()
                {
                    Id = entity.ExerInWorkoutId,
                    ExerciseId = entity.ExerInWorkout.ExerciseId,
                    WorkoutId = entity.ExerInWorkout.WorkoutId,
                    Workout = entity.ExerInWorkout.Workout != null
                        ? new Workout()
                        {
                            Id = entity.ExerInWorkout.WorkoutId,
                            Name = entity.ExerInWorkout.Workout?.Name!,
                            Date = entity.ExerInWorkout.Workout?.Date,
                            Public = entity.ExerInWorkout.Workout!.Public!,
                            Users = entity.ExerInWorkout.Workout!.Users?.Select(s => new UsersInWorkout()
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

    public Domain.EF.SetInExerc? Map(SetInExerc? entity)
    {
        if (entity == null) return null;
        return new Domain.EF.SetInExerc()
        {
            Id = entity.Id,
            Reps = entity.Reps,
            ExerInWorkoutId = entity.ExerInWorkoutId,
            Weight = entity.Weight,
        };
    }
}