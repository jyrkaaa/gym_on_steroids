using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class ExerTargerRepository : BaseRepository<App.DAL.DTO.ExerTarget, App.Domain.EF.ExerTarget> , IExerTargetRepository
{
    public ExerTargerRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ExerTargetUOWMapper())
    {
    }

    public IEnumerable<ExerTarget> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ExerTarget>> AllAsync(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public ExerTarget? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<ExerTarget?> FindAsync(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public void Add(ExerTarget entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public ExerTarget Update(ExerTarget entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(ExerTarget entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}