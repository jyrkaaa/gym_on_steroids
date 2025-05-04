using Base.BLL.Contracts;

namespace App.DTO.v1.Mappers;

public class WorkoutV1Mapper : IBLLMapper<App.DTO.v1.Workout, App.BLL.DTO.Workout>
{
    public Workout? Map(BLL.DTO.Workout? entity)
    {
        if (entity == null) return null;
        return new Workout()
        {
            Id = entity.Id,
            Name = entity.Name,
            Date = entity.Date,
            Exercises = entity.Exercises?.Select(e => new ExerInWorkout()
            {
                Id = e.Id,
                WorkoutId = e.WorkoutId,
                ExerciseId = e.ExerciseId
            }).ToList(),
            
            Public = entity.Public,
            Users = entity.Users?.Select(u => new UsersInWorkout()
            {
                Id = u.Id,
                WorkoutId = u.WorkoutId,
                NetUserId = u.NetUserId,
            }).ToList()
        };
    }

    public BLL.DTO.Workout? Map(Workout? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.Workout()
        {
            Id = entity.Id,
            Name = entity.Name,
            Date = entity.Date,
            Exercises = entity.Exercises?.Select(e => new BLL.DTO.ExerInWorkout()
            {
                Id = e.Id,
                WorkoutId = e.WorkoutId,
                ExerciseId = e.ExerciseId
            }).ToList(),
            
            Public = entity.Public,
            Users = entity.Users?.Select(u => new BLL.DTO.UsersInWorkout()
            {
                Id = u.Id,
                WorkoutId = u.WorkoutId,
                NetUserId = u.NetUserId,
            }).ToList()
        };
    }
}