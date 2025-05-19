using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class ExerTargetUOWMapper : IMapper<App.DAL.DTO.ExerTarget, App.Domain.EF.ExerTarget>
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