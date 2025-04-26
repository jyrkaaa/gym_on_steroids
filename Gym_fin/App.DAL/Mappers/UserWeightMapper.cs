using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class UserWeightMapper : IUOWMapper<App.DAL.DTO.UserWeight, App.Domain.EF.UserWeight>
{
    public UserWeight? Map(Domain.EF.UserWeight? entity)
    {
        throw new NotImplementedException();
    }

    public Domain.EF.UserWeight? Map(UserWeight? entity)
    {
        throw new NotImplementedException();
    }
}