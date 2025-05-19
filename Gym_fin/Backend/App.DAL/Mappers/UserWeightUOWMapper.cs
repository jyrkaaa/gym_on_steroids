using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class UserWeightUOWMapper : IMapper<App.DAL.DTO.UserWeight, App.Domain.EF.UserWeight>
{
    public UserWeight? Map(Domain.EF.UserWeight? entity)
    {
        if (entity == null) return null;
        return new UserWeight()
        {
            Id = entity.Id,
            WeightKg = entity.WeightKg,
            Date = entity.Date,
            Desc = entity.Desc,
            NetUserId = entity.NetUserId,
            NetUser = null,
        };
    }

    public Domain.EF.UserWeight? Map(UserWeight? entity)
    {
        if (entity == null) return null;
        return new Domain.EF.UserWeight()
        {
            Id = entity.Id,
            WeightKg = entity.WeightKg,
            Date = entity.Date,
            Desc = entity.Desc,
            NetUserId = entity.NetUserId,
            NetUser = null,
        };
    }
}