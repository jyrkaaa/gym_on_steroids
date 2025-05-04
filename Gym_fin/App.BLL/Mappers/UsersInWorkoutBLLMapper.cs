using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class UsersInWorkoutBLLMapper : IMapper<App.BLL.DTO.UsersInWorkout, App.DAL.DTO.UsersInWorkout>
{
    public UsersInWorkout? Map(DTO.UsersInWorkout? entity)
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

    public DTO.UsersInWorkout? Map(UsersInWorkout? entity)
    {
        if (entity == null) return null;
        return new DTO.UsersInWorkout()
        {
            Id = entity.Id,
            WorkoutId = entity.WorkoutId,
            NetUserId = entity.NetUserId,
            Workout = null,
            NetUser = null,
        };
    }
}