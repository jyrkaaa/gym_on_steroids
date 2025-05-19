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

public class UserWeightService : BaseService<App.BLL.DTO.UserWeight, App.DAL.DTO.UserWeight, App.DAL.Contracts.IUserWeightRepository>, IUserWeightService
{
    public UserWeightService(
        IAppUOW serviceUOW,
        IMapper<DTO.UserWeight, App.DAL.DTO.UserWeight> mapper) : base(serviceUOW, serviceUOW.UserWeightRepository, mapper)
    {
    }
}