
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

public class UsersInWorkoutService : BaseService<App.BLL.DTO.UsersInWorkout, App.DAL.DTO.UsersInWorkout, App.DAL.Contracts.IUsersInWorkoutRepository>, IUsersInWorkoutService
{
    public UsersInWorkoutService(
        IAppUOW serviceUOW,
        IMapper<DTO.UsersInWorkout, App.DAL.DTO.UsersInWorkout> mapper) : base(serviceUOW, serviceUOW.UsersInWorkoutRepository, mapper)
    {
    }

    public async Task<bool> FindByWorkoutsAsync(Guid? workoutId, Guid? eiwId, Guid userId, bool publicWorkout)
    {
        var response = await ServiceRepository.FindByWorkoutsAsync(workoutId, eiwId, userId, publicWorkout);
        return response;
    }
    
}
