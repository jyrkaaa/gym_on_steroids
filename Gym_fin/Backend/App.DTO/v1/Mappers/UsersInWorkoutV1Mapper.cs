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
            Workout = entity.WorkoutId != null ? new Workout()
            {
                Id = entity.Workout!.Id,
                Date = entity.Workout!.Date,
                Name = entity.Workout.Name,
                Public = entity.Workout.Public,
            } : null,
            NetUser = entity.NetUser != null ? new AppUser() { Id = entity.NetUser.Id } : null,
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
            Workout = entity.WorkoutId != null ? new BLL.DTO.Workout()
            {
                Id = entity.Workout!.Id,
                Date = entity.Workout!.Date,
                Name = entity.Workout.Name,
                Public = entity.Workout.Public,
            } : null,
            NetUser = entity.NetUser != null ? new BLL.DTO.AppUser() { Id = entity.NetUser.Id } : null,
        };
    }
}