using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IAppBLL : IBaseBLL
{
    IExerciseService ExerciseService { get; }
    IExerciseCategoryService ExerciseCategoryService { get; }
    IUsersInWorkoutService UsersInWorkoutService { get; }
    IWorkoutService WorkoutService { get; }
    IExerInWorkoutService ExerInWorkoutService { get; }
    ISetInExercService SetInExercService { get; }
    IUserWeightService UserWeightService { get; }
}