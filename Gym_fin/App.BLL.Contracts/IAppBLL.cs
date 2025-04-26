using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IAppBLL : IBaseBLL
{
    IExerciseService ExerciseService { get; }
    IExerciseCategoryService ExerciseCategoryService { get; }
}