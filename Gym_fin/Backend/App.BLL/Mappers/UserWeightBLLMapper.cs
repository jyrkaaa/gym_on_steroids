using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class UserWeightBLLMapper : IMapper<App.BLL.DTO.UserWeight, App.DAL.DTO.UserWeight>
{
    public UserWeight? Map(DTO.UserWeight? entity)
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

    public DTO.UserWeight? Map(UserWeight? entity)
    {
        if (entity == null) return null;
        return new DTO.UserWeight()
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