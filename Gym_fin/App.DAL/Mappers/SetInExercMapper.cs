using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class SetInExercMapper : IUOWMapper<App.DAL.DTO.SetInExerc, App.Domain.EF.SetInExerc>
{
    public SetInExerc? Map(Domain.EF.SetInExerc? entity)
    {
        throw new NotImplementedException();
    }

    public Domain.EF.SetInExerc? Map(SetInExerc? entity)
    {
        throw new NotImplementedException();
    }
}