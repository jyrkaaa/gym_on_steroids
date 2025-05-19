using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class ExerGuideRepository : BaseRepository<App.DAL.DTO.ExerGuide, App.Domain.EF.ExerGuide> , IExerGuideRepository
{
    public ExerGuideRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ExerGuideUOWMapper())
    {
    }

    public IEnumerable<ExerGuide> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ExerGuide>> AllAsync(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public ExerGuide? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<ExerGuide?> FindAsync(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public void Add(ExerGuide entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public ExerGuide Update(ExerGuide entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(ExerGuide entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}