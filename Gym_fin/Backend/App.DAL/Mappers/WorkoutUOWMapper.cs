using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class WorkoutUOWMapper : IMapper<App.DAL.DTO.Workout, App.Domain.EF.Workout>
{
    public Workout? Map(Domain.EF.Workout? entity)
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
                    Username = u.NetUser?.UserName
                } : null
            }).ToList()
        };
    }

    public Domain.EF.Workout? Map(Workout? entity)
    {
        if (entity == null) return null;
        return new Domain.EF.Workout()
        {
            Id = entity.Id,
            Name = entity.Name,
            Date = entity.Date,
            Exercises = entity.Exercises?.Select(e => new Domain.EF.ExerInWorkout()
            {
                Id = e.Id,
                WorkoutId = e.WorkoutId,
                ExerciseId = e.ExerciseId
            }).ToList(),
            Public = entity.Public,
            Users = entity.Users?.Select(u => new Domain.EF.UsersInWorkout()
            {
                Id = u.Id,
                WorkoutId = u.WorkoutId,
                NetUserId = u.NetUserId,
            }).ToList()
        };
    }
}