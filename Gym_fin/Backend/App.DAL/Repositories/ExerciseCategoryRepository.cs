using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace App.DAL.Repositories;

public class ExerciseCategoryRepository : BaseRepository<DTO.ExerciseCategory, App.Domain.EF.ExerciseCategory> , IExerciseCategoryRepository
{
    public ExerciseCategoryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ExerciseCategoryUOWMapper())
    {
    }

    public IEnumerable<DTO.ExerciseCategory> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<DTO.ExerciseCategory>> AllAsync(Guid userId = default)
    {
        var query = GetQuery()
            .Include(c => c.Exercises);
        return (await query.ToListAsync()).Select(c => Mapper.Map(c!));;
    }

    public DTO.ExerciseCategory? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<DTO.ExerciseCategory?> FindAsync(Guid id, Guid userId = default)
    {
        var query = await GetQuery()
            .Include(e => e.Exercises)
            .FirstOrDefaultAsync(e => e.Id == id);
        return Mapper.Map(query);
    }

    public override void Add(DTO.ExerciseCategory entity, Guid userId = default)
    {
        var efEntity = Mapper.Map(entity);
        if (efEntity == null) return;
        
        RepositoryDbSet.Add(efEntity);
        
    }

    public DTO.ExerciseCategory Update(ExerciseCategory entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(ExerciseCategory entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}