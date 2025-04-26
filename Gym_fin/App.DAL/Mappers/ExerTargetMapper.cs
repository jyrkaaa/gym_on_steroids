using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class ExerTargetMapper : IUOWMapper<App.DAL.DTO.ExerTarget, App.Domain.EF.ExerTarget>
{
    public ExerTarget? Map(Domain.EF.ExerTarget? entity)
    {
        throw new NotImplementedException();
    }

    public Domain.EF.ExerTarget? Map(ExerTarget? entity)
    {
        throw new NotImplementedException();
    }
}