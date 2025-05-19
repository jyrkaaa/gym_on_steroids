using App.DAL.Contracts;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface ISetInExercService : IBaseService<App.BLL.DTO.SetInExerc>, ISetCustomRepository
{
}

public interface ISetCustomRepository
{
    public Task<DTO.SetInExerc?> FindBiggestWeight(Guid exerciseId, Guid userId);

}


