using Base.BLL.Contracts;

namespace App.DTO.v1.Mappers;

public class UserWeightV1Mapper : IBLLMapper<App.DTO.v1.UserWeight, App.BLL.DTO.UserWeight>
{
    public UserWeight? Map(BLL.DTO.UserWeight? entity)
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

    public BLL.DTO.UserWeight? Map(UserWeight? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.UserWeight()
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