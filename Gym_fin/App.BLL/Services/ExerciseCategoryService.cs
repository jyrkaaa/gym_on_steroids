
using Base.BLL;
using App.BLL.Contracts;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.DAL.Contracts;
using App.DAL.DTO;


namespace App.BLL.Services;

public class ExerciseCategoryService : BaseService<App.BLL.DTO.ExerciseCategory, App.DAL.DTO.ExerciseCategory, App.DAL.Contracts.IExerciseCategoryRepository>, IExerciseCategoryService
{
    public ExerciseCategoryService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.ExerciseCategory, ExerciseCategory> bllMapper) : base(serviceUOW, serviceUOW.ExerciseCategoryRepository, bllMapper)
    {
    }
}
