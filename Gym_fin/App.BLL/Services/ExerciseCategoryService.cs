
using Base.BLL;
using App.BLL.Contracts;
using App.BLL.Mappers;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.DAL.Contracts;
using App.DAL.DTO;
using Base.Contracts;


namespace App.BLL.Services;

public class ExerciseCategoryService : BaseService<App.BLL.DTO.ExerciseCategory, App.DAL.DTO.ExerciseCategory, App.DAL.Contracts.IExerciseCategoryRepository>, IExerciseCategoryService
{
    public ExerciseCategoryService(
        IAppUOW serviceUOW,
        IMapper<DTO.ExerciseCategory, ExerciseCategory> mapper) : base(serviceUOW, serviceUOW.ExerciseCategoryRepository, mapper)
    {
    }
}
