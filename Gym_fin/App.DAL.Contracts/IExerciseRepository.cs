using App.Domain.EF;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IExerciseRepository : IBaseRepository<App.DAL.DTO.Exercise>, IExerciseRepositoryCustom
{
    Task<IEnumerable<App.DAL.DTO.Exercise>> GetAllByCategoryIdAsync(Guid categoryId, Guid userId);
}

public interface IExerciseRepositoryCustom
{
    Task<IEnumerable<App.DAL.DTO.Exercise>> GetAllByCategoryIdAsync(Guid categoryId, Guid userId);

}