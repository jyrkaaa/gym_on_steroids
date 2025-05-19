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

    public virtual async Task<IEnumerable<DTO.Exercise>> AllAsync(Guid? userId, Guid? categoryId)
    {
        return (await ServiceRepository.AllAsync(userId, categoryId)).Select(exercise => Mapper.Map(exercise!));
    }
}
