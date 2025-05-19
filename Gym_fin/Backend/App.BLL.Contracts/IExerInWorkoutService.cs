using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Contracts;

public interface IExerInWorkoutService : IBaseService<App.BLL.DTO.ExerInWorkout>, ICustomEiwService
{
    
}

public interface ICustomEiwService
{
    public Task RemoveAsync(Guid id, Guid? userId = default);
}