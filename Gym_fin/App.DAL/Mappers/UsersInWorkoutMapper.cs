using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class UsersInWorkoutMapper : IUOWMapper<App.DAL.DTO.UsersInWorkout, App.Domain.EF.UsersInWorkout>
{
    public UsersInWorkout? Map(Domain.EF.UsersInWorkout? entity)
    {
        throw new NotImplementedException();
    }

    public Domain.EF.UsersInWorkout? Map(UsersInWorkout? entity)
    {
        throw new NotImplementedException();
    }
}