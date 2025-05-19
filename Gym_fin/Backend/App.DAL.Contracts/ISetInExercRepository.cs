using App.Domain.EF;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface ISetInExercRepository : IBaseRepository<App.DAL.DTO.SetInExerc>, ISetCustomRepository
{
    
}

public interface ISetCustomRepository
{
    public Task<DTO.SetInExerc?> FindBiggestWeight(Guid exerciseId, Guid userId);

}