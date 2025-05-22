using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Contracts;

public interface IWorkoutService : IBaseService<App.BLL.DTO.Workout>, IWorkoutRepositoryCustom
{
    
}
public interface IWorkoutRepositoryCustom
{
    Task<IEnumerable<App.BLL.DTO.Workout>> AllAsync(Guid? userId, string? name, DateTimeOffset? fromDate, DateTimeOffset? toDate);
    Task<bool> PatchWorkoutAsync(Guid id, Guid userId, bool publicState);
    Task<IEnumerable<App.BLL.DTO.Workout>> AllAsyncExercise(Guid exerciseId, Guid userId);
}