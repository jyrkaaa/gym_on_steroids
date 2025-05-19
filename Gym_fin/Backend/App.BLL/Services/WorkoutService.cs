using Base.BLL;
using App.BLL.Contracts;
using App.BLL.Mappers;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.DAL.Contracts;
using App.DAL.DTO;
using Base.Contracts;


namespace App.BLL.Services;

public class WorkoutService : BaseService<App.BLL.DTO.Workout, App.DAL.DTO.Workout, App.DAL.Contracts.IWorkoutRepository>, IWorkoutService
{
    public WorkoutService(
        IAppUOW serviceUOW,
        IMapper<DTO.Workout, App.DAL.DTO.Workout> mapper) : base(serviceUOW, serviceUOW.WorkoutRepository, mapper)
    {
    }
    public virtual async Task<IEnumerable<DTO.Workout>> AllAsync(Guid? userId, string? name, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
    {
        return (await ServiceRepository.AllAsync(userId!.Value, name, dateFrom, dateTo)).Select(w => Mapper.Map(w!));
        
    }

    public async Task<bool> PatchWorkoutAsync(Guid id, Guid userId, bool publicState)
    {
        var response = await ServiceRepository.PatchWorkoutAsync(id, userId, publicState);
        return response; 
    }
}