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
            Workout = entity.Workout != null ? new Workout()
            {
                Id = entity.Workout!.Id,
                Date = entity.Workout!.Date,
                Name = entity.Workout.Name,
                Public = entity.Workout.Public,
            } : null,
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
            Workout = entity.WorkoutId != null ? new DTO.Workout()
            {
                Id = entity.Workout!.Id,
                Date = entity.Workout!.Date,
                Name = entity.Workout.Name,
                Public = entity.Workout.Public,
            } : null,
            NetUser = entity.NetUser != null ? new DTO.AppUser() { Id = entity.NetUser.Id } : null,

        };
    }
}