using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class UsersInWorkoutUOWMapper : IMapper<App.DAL.DTO.UsersInWorkout, App.Domain.EF.UsersInWorkout>
{
    public UsersInWorkout? Map(Domain.EF.UsersInWorkout? entity)
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

    public Domain.EF.UsersInWorkout? Map(UsersInWorkout? entity)
    {
        if (entity == null) return null;
        return new Domain.EF.UsersInWorkout()
        {
            Id = entity.Id,
            WorkoutId = entity.WorkoutId,
            NetUserId = entity.NetUserId,
            Workout = null,
            NetUser = null,
        };
    }
}