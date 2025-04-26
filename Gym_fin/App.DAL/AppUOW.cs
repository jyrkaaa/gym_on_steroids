using Base.DAL.EF;
using App.DAL.Contracts;
using App.DAL.Repositories;

namespace App.DAL;

public class AppUOW : BaseUOW<AppDbContext> , IAppUOW
    
{
    public AppUOW(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }

    private IExerciseRepository? _exerciseRepository;
    public IExerciseRepository ExerciseRepository => _exerciseRepository ??= new ExerciseRepository(UOWDbContext);
    
    private IExerciseCategoryRepository? _exerciseCategoryRepository;
    public IExerciseCategoryRepository ExerciseCategoryRepository => _exerciseCategoryRepository ??= new ExerciseCategoryRepository(UOWDbContext);

    private IUserWeightRepository? _userWeightRepository;
    public IUserWeightRepository UserWeightRepository => _userWeightRepository ??= new UserWeightRepository(UOWDbContext);

    private IWorkoutRepository? _workoutRepository;
    public IWorkoutRepository WorkoutRepository => _workoutRepository ??= new WorkoutRepository(UOWDbContext);

    private IUsersInWorkoutRepository? _usersInWorkoutRepository;
    public IUsersInWorkoutRepository UsersInWorkoutRepository => _usersInWorkoutRepository ??= new UsersInWorkoutRepository(UOWDbContext);

    private IExerGuideRepository? _exerGuideRepository;
    public IExerGuideRepository ExerGuideRepository => _exerGuideRepository ??= new ExerGuideRepository(UOWDbContext);

    private ISetInExercRepository? _setInExercRepository;
    public ISetInExercRepository SetInExercRepository => _setInExercRepository ??= new SetInExercRepository(UOWDbContext);

    private IExerTargetRepository? _exerTargetRepository;
    public IExerTargetRepository ExerTargetRepository => _exerTargetRepository ??= new ExerTargerRepository(UOWDbContext);

    private IExerInWorkoutRepository? _exerInWorkoutRepository;
    public IExerInWorkoutRepository ExerInWorkoutRepository => _exerInWorkoutRepository ??= new ExerInWorkoutRepository(UOWDbContext);
}