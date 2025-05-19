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
                ExerciseId = e.ExerciseId,
                Exercise = e.Exercise != null ? new Exercise() {Id = e.ExerciseId, ExerciseCategoryId = e.Exercise!.ExerciseCategoryId, Name = e.Exercise.Name, Date = e.Exercise.Date} : null,
                Sets = e.Sets?.Select(s => new SetInExerc()
                {
                    Id = s.Id,
                    Weight = s.Weight,
                    Reps = s.Reps,
                    ExerInWorkoutId = s.ExerInWorkoutId,
                }).ToList(),
            }).ToList(),
            
            Public = entity.Public,
            Users = entity.Users?.Select(u => new UsersInWorkout()
            {
                Id = u.Id,
                WorkoutId = u.WorkoutId,
                NetUserId = u.NetUserId,
                NetUser = u.NetUser != null ? new AppUser()
                {
                    Id = u.NetUserId!.Value,
                    Email = u.NetUser?.Email,
                    Username = u.NetUser?.Username,
                } : null
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

    public BLL.DTO.Workout? Map(App.DTO.v1.WorkoutCreate entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.Workout()
        {
            Id = Guid.NewGuid(),
            Date = entity.Date,
            Name = entity.Name,
            Public = entity.Public,
            
        };
    }

    public BLL.DTO.Workout? Map(App.DTO.v1.WorkoutEdit entity)
    {
        return new BLL.DTO.Workout()
        {
            Id = entity.Id,
            Date = entity.Date,
            Name = entity.Name,
            Public = entity.Public,
        };
    }
}