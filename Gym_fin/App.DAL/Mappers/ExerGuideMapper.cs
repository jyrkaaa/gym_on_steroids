using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class ExerGuideMapper : IUOWMapper<App.DAL.DTO.ExerGuide, App.Domain.EF.ExerGuide>
{
    public ExerGuide? Map(Domain.EF.ExerGuide? entity)
    {
        throw new NotImplementedException();
    }

    public Domain.EF.ExerGuide? Map(ExerGuide? entity)
    {
        throw new NotImplementedException();
    }
}