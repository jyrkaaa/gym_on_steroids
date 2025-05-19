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

    public BLL.DTO.SetInExerc? Map(SetInExerc? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.SetInExerc()
        {
            Id = entity.Id,
            ExerInWorkoutId = entity.ExerInWorkoutId,
            Weight = entity.Weight,
            Reps = entity.Reps,
            ExerInWorkout = new BLL.DTO.ExerInWorkout()
            {
                Id = entity.ExerInWorkoutId,
                ExerciseId = entity.ExerInWorkoutId,
            }
        };
    }

    public BLL.DTO.SetInExerc? Map(SetInExercCreate? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.SetInExerc()
        {
            Id = Guid.NewGuid(),
            ExerInWorkoutId = entity.ExerInWorkoutId,
            Weight = entity.Weight,
            Reps = entity.Reps,
        };
    }
}