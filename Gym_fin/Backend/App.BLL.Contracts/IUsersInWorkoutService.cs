using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Contracts;

public interface IUsersInWorkoutService : IBaseService<App.BLL.DTO.UsersInWorkout>, ICustomUiwRepository
{
}

public interface ICustomUiwRepository
{
    Task<bool> FindByWorkoutsAsync(Guid? workoutId,Guid? eiwId, Guid userId, bool publicWorkout);
}

