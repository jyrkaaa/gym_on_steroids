using Base.BLL.Contracts;

namespace App.DTO.v1.Mappers;

public class UsersInWorkoutV1Mapper : IBLLMapper<App.DTO.v1.UsersInWorkout, App.BLL.DTO.UsersInWorkout>
{
    public UsersInWorkout? Map(BLL.DTO.UsersInWorkout? entity)
    {
        if (entity == null) return null;
        return new UsersInWorkout()
        {
            Id = entity.Id,
            WorkoutId = entity.WorkoutId,
            NetUserId = entity.NetUserId,
            Workout = null,
            NetUser = null,
        };
    }

    public BLL.DTO.UsersInWorkout? Map(UsersInWorkout? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.UsersInWorkout()
        {
            Id = entity.Id,
            WorkoutId = entity.WorkoutId,
            NetUserId = entity.NetUserId,
            Workout = null,
            NetUser = null,
        };
    }
}