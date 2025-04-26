using App.Domain.EF;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IExerciseRepository : IBaseRepository<App.DAL.DTO.Exercise>, IExerciseRepositoryCustom
{
    
}

public interface IExerciseRepositoryCustom
{
}