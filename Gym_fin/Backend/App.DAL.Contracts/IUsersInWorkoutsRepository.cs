using App.Domain.EF;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IUsersInWorkoutRepository : IBaseRepository<App.DAL.DTO.UsersInWorkout>, ICustomUiwRepository
{
    
}

public interface ICustomUiwRepository
{
    public Task<bool> FindByWorkoutsAsync(Guid? workoutId, Guid? eiwId, Guid userId, bool publicWorkout);
}