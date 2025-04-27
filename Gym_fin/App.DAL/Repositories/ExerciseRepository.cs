using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Exercise = App.DAL.DTO.Exercise;

namespace App.DAL.Repositories;

public class ExerciseRepository : BaseRepository<DTO.Exercise ,App.Domain.EF.Exercise> , IExerciseRepository
{
    public ExerciseRepository(AppDbContext repositoryDBContext) : base(repositoryDBContext, new ExerciseUOWMapper())
    {
    }


    public override async Task<IEnumerable<DTO.Exercise>> AllAsync(Guid userId = default)
    {
        var query = GetQuery(userId);
        query = query
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts);

        return (await query.ToListAsync()).Select(e => UOWMapper.Map(e)!);
    }

    public override IEnumerable<DTO.Exercise> All(Guid userId = default)
    {
        var query = GetQuery(userId);
        query = query
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts);

        return query.ToList().Select(e => UOWMapper.Map(e)!);
    }

    public override async Task<DTO.Exercise?> FindAsync( Guid id, Guid userId = default!)
    {
        var entity = await GetQuery(userId)
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts)
            .FirstOrDefaultAsync(e => e.Id == id);

        return UOWMapper.Map(entity);
    }

    public override DTO.Exercise? Find(Guid id, Guid userId = default)
    {
        var entity = GetQuery(userId)
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts)
            .FirstOrDefault(e => e.Id == id);

        return UOWMapper.Map(entity);
    }

    public override void Add(DTO.Exercise entity, Guid userId = default)
    {
        var domainEntity = UOWMapper.Map(entity)!;
        RepositoryDbSet.Add(domainEntity);
    }

    public override DTO.Exercise Update(DTO.Exercise entity)
    {
        var domainEntity = UOWMapper.Map(entity)!;
        RepositoryDbSet.Update(domainEntity);
        return entity;
    }

    public override void Remove(DTO.Exercise entity, Guid userId = default)
    {
        var domainEntity = UOWMapper.Map(entity)!;
        RepositoryDbSet.Remove(domainEntity);
    }

    public async Task<IEnumerable<DTO.Exercise>> GetAllByCategoryIdAsync(Guid categoryId, Guid userId)
    {
        var query = GetQuery(userId)
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts)
            .Where(e => e.ExerciseCategoryId == categoryId);
        var entities = await query.ToListAsync();
        return entities.Select(e => UOWMapper.Map(e))!;
    }
    
}

