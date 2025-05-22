using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class WorkoutBLLMapper : IMapper<App.BLL.DTO.Workout, App.DAL.DTO.Workout>
{
    public Workout? Map(DTO.Workout? entity)
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

    public DTO.Workout? Map(Workout? entity)
    {
        if (entity == null) return null;
        return new DTO.Workout()
        {
            Id = entity.Id,
            Name = entity.Name,
            Date = entity.Date,
            CreatedBy = entity.CreatedBy,
            Exercises = entity.Exercises?.Select(e => new DTO.ExerInWorkout()
            {
                Id = e.Id,
                WorkoutId = e.WorkoutId,
                ExerciseId = e.ExerciseId,
                Exercise = e.Exercise != null ? new DTO.Exercise() {Id = e.ExerciseId, ExerciseCategoryId = e.Exercise!.ExerciseCategoryId, Name = e.Exercise.Name, Date = e.Exercise.Date} : null,
                Sets = e.Sets?.Select(s => new DTO.SetInExerc()
                {
                    Id = s.Id,
                    Weight = s.Weight,
                    Reps = s.Reps,
                    ExerInWorkoutId = s.ExerInWorkoutId,
                }).ToList(),
            }).ToList(),
            
            Public = entity.Public,
            Users = entity.Users?.Select(u => new DTO.UsersInWorkout()
            {
                Id = u.Id,
                WorkoutId = u.WorkoutId,
                NetUserId = u.NetUserId,
                NetUser = u.NetUser != null ? new DTO.AppUser()
                {
                    Id = u.NetUserId!.Value,
                    Email = u.NetUser?.Email,
                    Username = u.NetUser?.Username
                } : null
            }).ToList()
        };
    }
}