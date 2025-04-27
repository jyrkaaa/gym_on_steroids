
using Base.BLL;
using App.BLL.Contracts;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL.Contracts;


namespace App.BLL.Services;

public class ExerciseService : BaseService<App.BLL.DTO.Exercise, App.DAL.DTO.Exercise, App.DAL.Contracts.IExerciseRepository>, IExerciseService
{
    public ExerciseService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Exercise, Exercise> bllMapper) : base(serviceUOW, serviceUOW.ExerciseRepository, bllMapper)
    {
        
    }
    public async Task<IEnumerable<App.DAL.DTO.Exercise>> GetAllByCategoryIdAsync(Guid categoryId, Guid userId)
    {
        var exercises = await ServiceRepository.GetAllByCategoryIdAsync(categoryId, userId);

        return exercises;
    }
}
