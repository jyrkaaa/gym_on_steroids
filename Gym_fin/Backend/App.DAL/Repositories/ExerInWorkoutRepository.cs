using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class ExerInWorkoutRepository : BaseRepository<App.DAL.DTO.ExerInWorkout, App.Domain.EF.ExerInWorkout> , IExerInWorkoutRepository
{
    public ExerInWorkoutRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ExerInWorkoutUOWMapper())
    {
    }

    public IEnumerable<ExerInWorkout> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<DTO.ExerInWorkout>> AllAsync(Guid userId = default)
    {
        return (await RepositoryDbSet
            .Include(e => e.Sets)
            .Include(e => e.Exercise)
            .Include(e => e.Workout)
            .ThenInclude(w => w!.Users)
            .Where(e => e.Workout!.Users.Any(u => u.NetUserId == userId))
        .ToListAsync()).Select(e => Mapper.Map(e));
    }

    public ExerInWorkout? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<DTO.ExerInWorkout?> FindAsync(Guid id, Guid userId = default)
    {
        return Mapper.Map(await RepositoryDbSet
            .Include(e => e.Sets)
            .Include(e => e.Exercise)
            .Include(e => e.Workout)
            .ThenInclude(w => w!.Users)
            .Where(e => e.Workout!.Users.Any(u => u.NetUserId == userId))
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync());
    }

    public void Add(ExerInWorkout entity, Guid userId = default)
    {
        throw new NotImplementedException();

    }

    public ExerInWorkout Update(ExerInWorkout entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id, Guid? userId = default)
    {
        var entity  = await RepositoryDbSet.Include(e => e.Workout)
            .ThenInclude(w => w!.Users)
            .FirstOrDefaultAsync(w => w.Workout!.Users.Any(u => u.NetUserId == userId) && w.Id == id);
        if (entity != null)
        {
            RepositoryDbSet.Remove(entity);
        }
            
    }
}
