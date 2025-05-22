using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class WorkoutRepository : BaseRepository<App.DAL.DTO.Workout, App.Domain.EF.Workout>, IWorkoutRepository
{
    public WorkoutRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new WorkoutUOWMapper())
    {
    }

    public IEnumerable<Workout> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<DTO.Workout>> AllAsync(Guid userId, string? name, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
    {
        var query = GetQuery(userId);
        query = query.Include(w => w.Exercises)
            .Include(w => w!.Users)
            .ThenInclude(u => u.NetUser)
            .OrderByDescending(w => w.Date);
        ;
        if (dateFrom.HasValue)
        {
            var utcFrom = dateFrom.Value.ToUniversalTime();
            query = query.Where(w => w.Date >= utcFrom);
        }
        if (dateTo.HasValue)
        {
            var utcFrom = dateTo.Value.ToUniversalTime();

            query = query.Where(w => w.Date <= utcFrom);
        }
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(w => (w.Name).ToUpper().Contains(name.ToUpper()))
                .Where(w => w.Public == true || w.Users!.Any(u => u.NetUserId == userId));
        }
        else
        {
            query = query.Where(w => w.Users!.Any(x => x.NetUserId == userId));

        }
        return (await query.ToListAsync()).Select(w => Mapper.Map(w!));
    }

    public async Task<bool> PatchWorkoutAsync(Guid id, Guid userId, bool publicWorkout)
    {
        try
        {
            var efEntity =
                 await RepositoryDbSet
                     
                     .Include(w => w.Users)
                     .Where(w => w.Id == id)
                     .Where(w => w.Users!.Any(u => u.NetUserId == userId))
                     .FirstOrDefaultAsync();
            if (efEntity == null) return false;

            if (!efEntity.Public.Equals(publicWorkout))
            {
                efEntity.Public = publicWorkout;
            }

            RepositoryDbContext.Update(efEntity);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<IEnumerable<DTO.Workout>> AllAsyncExercise(Guid exerciseId, Guid userId)
    {
        var query = GetQuery(userId);
        query = query
            .Include(w => w!.Exercises)
                .ThenInclude(e => e.Exercise)
            .Include(w => w!.Exercises)
                .ThenInclude(e => e.Sets)
            .Include(w => w!.Users)
                .ThenInclude(u => u.NetUser)
            .OrderByDescending(w => w.Date)
            .Where(w => w.Exercises!.Any(e => e.ExerciseId == exerciseId) && w.Users!.Any(u => u.NetUserId == userId));
            ;
        return (await query.ToListAsync()).Select(w => Mapper.Map(w!));
    }

    public Workout? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<DTO.Workout?> FindAsync(Guid id, Guid userId = default)
    {
        return Mapper.Map(await RepositoryDbSet
            .Include(w => w!.Users)
                .ThenInclude(u => u.NetUser)
            .Include(w => w.Exercises!)
                .ThenInclude(e => e.Sets)
            .Include(w => w.Exercises!)
                .ThenInclude(e => e.Exercise)
            .Where(w => w.Id == id)
            .Where(w => w.Users!.Any(u => u.NetUserId == userId) || w.Public == true)
            .FirstOrDefaultAsync());
    }

    public override void Add(DTO.Workout entity, Guid userId = default)
    {
        RepositoryDbSet.Add(Mapper.Map(entity!));
    }

    public DTO.Workout? UpdateAsync(DTO.Workout entity)
    {
        var domainEntity = Mapper.Map(entity)!;
        RepositoryDbSet.Update(domainEntity);
        return Mapper.Map(domainEntity)!;
    }

    public void Remove(Workout entity, Guid userId = default)
    {
        RepositoryDbSet.Remove(entity);
    }
}