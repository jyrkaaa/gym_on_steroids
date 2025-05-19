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

public class SetInExercService : BaseService<App.BLL.DTO.SetInExerc, App.DAL.DTO.SetInExerc, App.DAL.Contracts.ISetInExercRepository>, ISetInExercService
{
    public SetInExercService(
        IAppUOW serviceUOW,
        IMapper<DTO.SetInExerc, App.DAL.DTO.SetInExerc> mapper) : base(serviceUOW, serviceUOW.SetInExercRepository, mapper)
    {
    }

    public async Task<DTO.SetInExerc?> FindBiggestWeight(Guid exerciseId, Guid userId)
    {
        var response = await ServiceRepository.FindBiggestWeight(exerciseId, userId);
        return response is null ? null : Mapper.Map(response);
    }
}