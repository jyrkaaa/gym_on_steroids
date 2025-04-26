using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class WorkoutMapper : IUOWMapper<App.DAL.DTO.Workout, App.Domain.EF.Workout>
{
    public Workout? Map(Domain.EF.Workout? entity)
    {
        throw new NotImplementedException();
    }

    public Domain.EF.Workout? Map(Workout? entity)
    {
        throw new NotImplementedException();
    }
}