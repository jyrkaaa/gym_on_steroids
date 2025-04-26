using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class ExerInWorkoutMapper : IUOWMapper<App.DAL.DTO.ExerInWorkout, App.Domain.EF.ExerInWorkout>
{
    public ExerInWorkout? Map(Domain.EF.ExerInWorkout? entity)
    {
        throw new NotImplementedException();
    }

    public Domain.EF.ExerInWorkout? Map(ExerInWorkout? entity)
    {
        throw new NotImplementedException();
    }
}