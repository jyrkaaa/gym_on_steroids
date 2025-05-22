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


    public async Task<IEnumerable<DTO.Exercise>> AllAsync(Guid? userId, Guid? categoryId)
    {
        var query = GetQuery();
        query = query
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts);
        if (categoryId != null)
        {
            query = query.Where(x => x.ExerciseCategoryId == categoryId.Value);
        }
        return (await query.ToListAsync()).Select(e => Mapper.Map(e)!);
    }
    public override IEnumerable<DTO.Exercise> All(Guid userId = default)
    {
        var query = GetQuery();
        query = query
            .Include(e => e.ExerciseCategoryId)
            .Include(e => e.ExerTargetId)
            .Include(e => e.ExerGuideId)
            .Include(e => e.ExerInWorkouts);

        return query.ToList().Select(e => Mapper.Map(e)!);
    }

    public override async Task<DTO.Exercise?> FindAsync( Guid id, Guid userId = default!)
    {
        var entity = await GetQuery()
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts)
            .FirstOrDefaultAsync(e => e.Id == id);

        return Mapper.Map(entity);
    }

    public override DTO.Exercise? Find(Guid id, Guid userId = default)
    {
        var entity = GetQuery(userId)
            .Include(e => e.ExerciseCategory)
            .Include(e => e.ExerTarget)
            .Include(e => e.ExerGuide)
            .Include(e => e.ExerInWorkouts)
            .FirstOrDefault(e => e.Id == id);

        return Mapper.Map(entity);
    }

    public override void Add(DTO.Exercise entity, Guid userId = default)
    {
        var domainEntity = Mapper.Map(entity)!;
        RepositoryDbSet.Add(domainEntity);
    }

    public DTO.Exercise Update(DTO.Exercise entity)
    {
        var domainEntity = Mapper.Map(entity)!;
        RepositoryDbSet.Update(domainEntity);
        return Mapper.Map(domainEntity)!;
    }

    public virtual async Task RemoveAsyncSafe(Guid exerciseId, Guid? userId = default)
    {
        if (userId != null)
        {
            var query = GetQuery(userId.Value!)
                .Where(e => e.Id == exerciseId && e.CreatedBy == userId.ToString());
            var deletableEntity = await query.FirstOrDefaultAsync();
            if (deletableEntity != null)
            {
                RepositoryDbSet.Remove(deletableEntity);
            }
        }
    }
    



}

