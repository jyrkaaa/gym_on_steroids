using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class SetInExercRepository : BaseRepository<App.DAL.DTO.SetInExerc, App.Domain.EF.SetInExerc> , ISetInExercRepository
{
    public SetInExercRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new SetInExercMapper())
    {
    }

    public IEnumerable<SetInExerc> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SetInExerc>> AllAsync(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public SetInExerc? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<SetInExerc?> FindAsync(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public void Add(SetInExerc entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public SetInExerc Update(SetInExerc entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(SetInExerc entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}