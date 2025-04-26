using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class ExerciseCategoryRepository : BaseRepository<App.DAL.DTO.ExerciseCategory, App.Domain.EF.ExerciseCategory> , IExerciseCategoryRepository
{
    public ExerciseCategoryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ExerciseCategoryUOWMapper())
    {
    }

    public IEnumerable<ExerciseCategory> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ExerciseCategory>> AllAsync(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public ExerciseCategory? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<ExerciseCategory?> FindAsync(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public void Add(ExerciseCategory entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public ExerciseCategory Update(ExerciseCategory entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(ExerciseCategory entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}