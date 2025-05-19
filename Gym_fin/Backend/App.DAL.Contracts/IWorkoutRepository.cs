using App.Domain.EF;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IWorkoutRepository : IBaseRepository<App.DAL.DTO.Workout>, IWorkoutRepositoryCustom
{
    
}

public interface IWorkoutRepositoryCustom
{
    public Task<IEnumerable<App.DAL.DTO.Workout>> AllAsync(Guid userId, string? name, DateTimeOffset? dateFrom, DateTimeOffset? dateTo);
    public Task<bool> PatchWorkoutAsync(Guid id, Guid userId, bool publicState);
}