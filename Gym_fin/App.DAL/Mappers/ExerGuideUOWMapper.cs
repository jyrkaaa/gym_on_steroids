using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class ExerGuideUOWMapper : IMapper<App.DAL.DTO.ExerGuide, App.Domain.EF.ExerGuide>
{
    public ExerGuide? Map(Domain.EF.ExerGuide? entity)
    {
        if (entity == null) return null;
        return new ExerGuide()
        {
            Id = entity.Id,
            Link = entity.Link,
            Exercises = null,
        };
    }

    public Domain.EF.ExerGuide? Map(ExerGuide? entity)
    {
        if (entity == null) return null;
        return new Domain.EF.ExerGuide()
        {
            Id = entity.Id,
            Link = entity.Link,
            Exercises = null,
        };
    }
}