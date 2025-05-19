using App.Domain.EF;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUOW : IBaseUOW
{
    IExerciseRepository ExerciseRepository { get; }
    IExerciseCategoryRepository ExerciseCategoryRepository { get; }
    IUserWeightRepository UserWeightRepository { get; }
    IWorkoutRepository WorkoutRepository { get; }
    IUsersInWorkoutRepository UsersInWorkoutRepository { get; }
    IExerGuideRepository ExerGuideRepository { get; }
    ISetInExercRepository SetInExercRepository { get; }
    IExerTargetRepository ExerTargetRepository { get; }
    IExerInWorkoutRepository ExerInWorkoutRepository { get; }
}