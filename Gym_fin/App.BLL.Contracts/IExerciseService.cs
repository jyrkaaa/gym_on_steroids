using App.DAL.Contracts;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IExerciseService : IBaseService<App.BLL.DTO.Exercise>, IExerciseRepositoryCustom
{
    
}

