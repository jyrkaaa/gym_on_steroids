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
    
    private IUsersInWorkoutService? _usersInWorkoutService;
    public IUsersInWorkoutService UsersInWorkoutService =>
        _usersInWorkoutService ??= new UsersInWorkoutService(BLLUOW, new UsersInWorkoutBLLMapper());
    
    private IWorkoutService? _workoutService;
    public IWorkoutService WorkoutService =>
        _workoutService ??= new WorkoutService(BLLUOW, new WorkoutBLLMapper());
    
    private IExerInWorkoutService? _exerInWorkoutService;
    public IExerInWorkoutService ExerInWorkoutService =>
        _exerInWorkoutService ??= new ExerInWorkoutService(BLLUOW, new ExerInWorkoutBLLMapper());
    
    private ISetInExercService? _setInExercService;
    public ISetInExercService SetInExercService =>
        _setInExercService ??= new SetInExercService(BLLUOW, new SetInExercBLLMapper());
    
    private IUserWeightService? _userWeightService;
    public IUserWeightService UserWeightService =>
        _userWeightService ??= new UserWeightService(BLLUOW, new UserWeightBLLMapper());
}
