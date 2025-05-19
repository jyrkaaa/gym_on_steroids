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

public class ExerInWorkoutService : BaseService<App.BLL.DTO.ExerInWorkout, App.DAL.DTO.ExerInWorkout, App.DAL.Contracts.IExerInWorkoutRepository>, IExerInWorkoutService
{
    public ExerInWorkoutService(
        IAppUOW serviceUOW,
        IMapper<DTO.ExerInWorkout, App.DAL.DTO.ExerInWorkout> mapper) : base(serviceUOW, serviceUOW.ExerInWorkoutRepository, mapper)
    {
    }

    public async Task RemoveAsync(Guid id, Guid? userId = default)
    {
        var response =  ServiceRepository.RemoveAsync(id, userId);
        
    }
}