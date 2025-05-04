using Base.BLL;
using App.BLL.Contracts;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;


namespace App.BLL.Services;

public class ExerciseService : BaseService<App.BLL.DTO.Exercise, App.DAL.DTO.Exercise, App.DAL.Contracts.IExerciseRepository>, IExerciseService
{
    public ExerciseService(
        IAppUOW serviceUOW,
        IMapper<DTO.Exercise, Exercise> mapper) : base(serviceUOW, serviceUOW.ExerciseRepository, mapper)
    {
    }
    public virtual async Task<IEnumerable<App.DAL.DTO.Exercise>> GetAllByCategoryIdAsync(Guid categoryId, Guid userId)
    {
        var exercises = await ServiceRepository.GetAllByCategoryIdAsync(categoryId, userId);

        return exercises.ToList();
    }
}
