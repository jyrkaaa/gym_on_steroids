using App.BLL.Contracts;
using App.BLL.Mappers;
using App.BLL.Services;
using App.DAL.Contracts;
using Base.BLL.Contracts;

namespace App.BLL;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    public AppBLL(IAppUOW uow) : base(uow)
    {
    }
    
    private IExerciseService? _exerciseService;
    public IExerciseService ExerciseService =>
        _exerciseService ??= new ExerciseService(BLLUOW, new ExerciseBLLMapper());
    
    private IExerciseCategoryService? _exerciseCategoryService;
    public IExerciseCategoryService ExerciseCategoryService =>
        _exerciseCategoryService ??= new ExerciseCategoryService(BLLUOW, new ExerciseCategoryBLLMapper());
}