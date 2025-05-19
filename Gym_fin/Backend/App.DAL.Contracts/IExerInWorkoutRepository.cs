using App.Domain.EF;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IExerInWorkoutRepository : IBaseRepository<App.DAL.DTO.ExerInWorkout>, ICustomEiwRepository
{
    
}

public interface ICustomEiwRepository
{
    public Task RemoveAsync(Guid id, Guid? userId);
}