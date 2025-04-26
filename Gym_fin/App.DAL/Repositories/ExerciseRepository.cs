using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Exercise = App.DAL.DTO.Exercise;

namespace App.DAL.Repositories;

public class ExerciseRepository : BaseRepository<App.DAL.DTO.Exercise ,App.Domain.EF.Exercise> , IExerciseRepository
{
    public ExerciseRepository(AppDbContext repositoryDBContext) : base(repositoryDBContext, new ExerciseUOWMapper())
    {
    }


    public override async Task<IEnumerable<App.DAL.DTO.Exercise>> AllAsync(Guid userId = default)
    {
        var query = GetQuery(userId);
        query = query
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts);

        return (await query.ToListAsync()).Select(e => UOWMapper.Map(e)!);
    }

    public override IEnumerable<App.DAL.DTO.Exercise> All(Guid userId = default)
    {
        var query = GetQuery(userId);
        query = query
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts);

        return query.ToList().Select(e => UOWMapper.Map(e)!);
    }

    public override async Task<Exercise?> FindAsync( Guid id, Guid userId = default!)
    {
        var entity = await GetQuery(userId)
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts)
            .FirstOrDefaultAsync(e => e.Id == id);

        return UOWMapper.Map(entity);
    }

    public override App.DAL.DTO.Exercise? Find(Guid id, Guid userId = default)
    {
        var entity = GetQuery(userId)
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts)
            .FirstOrDefault(e => e.Id == id);

        return UOWMapper.Map(entity);
    }

    public override void Add(App.DAL.DTO.Exercise entity, Guid userId = default)
    {
        var domainEntity = UOWMapper.Map(entity)!;
        RepositoryDbSet.Add(domainEntity);
    }

    public override App.DAL.DTO.Exercise Update(App.DAL.DTO.Exercise entity)
    {
        var domainEntity = UOWMapper.Map(entity)!;
        RepositoryDbSet.Update(domainEntity);
        return entity;
    }

    public override void Remove(App.DAL.DTO.Exercise entity, Guid userId = default)
    {
        var domainEntity = UOWMapper.Map(entity)!;
        RepositoryDbSet.Remove(domainEntity);
    }
}
