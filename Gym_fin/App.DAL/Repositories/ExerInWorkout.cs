using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class ExerInWorkoutRepository : BaseRepository<App.DAL.DTO.ExerInWorkout, App.Domain.EF.ExerInWorkout> , IExerInWorkoutRepository
{
    public ExerInWorkoutRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ExerInWorkoutMapper())
    {
    }

    public IEnumerable<ExerInWorkout> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ExerInWorkout>> AllAsync(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public ExerInWorkout? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<ExerInWorkout?> FindAsync(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public void Add(ExerInWorkout entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public ExerInWorkout Update(ExerInWorkout entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(ExerInWorkout entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}