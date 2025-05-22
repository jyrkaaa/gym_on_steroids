using App.DAL.Contracts;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IExerciseService : IBaseService<App.BLL.DTO.Exercise>, IExerciseServiceCustom
{

}

public interface IExerciseServiceCustom
{
    Task<IEnumerable<App.BLL.DTO.Exercise>> AllAsync(Guid? userId, Guid? categoryId);
    Task RemoveAsyncSafe(Guid exerciseId, Guid? userId);
}

