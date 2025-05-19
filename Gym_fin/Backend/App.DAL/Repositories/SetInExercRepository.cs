using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class SetInExercRepository : BaseRepository<App.DAL.DTO.SetInExerc, App.Domain.EF.SetInExerc> , ISetInExercRepository
{
    public SetInExercRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new SetInExercUOWMapper())
    {
    }

    public IEnumerable<SetInExerc> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<DTO.SetInExerc>> AllAsync(Guid userId = default)
    {
        return (await RepositoryDbSet
            .Include(s => s.ExerInWorkout)
                .ThenInclude(e => e!.Workout)
                    .ThenInclude(w => w!.Users)
            .Include(s => s.ExerInWorkout!.Exercise)
            .Where(s => s.ExerInWorkout!.Workout!.Users.Any(u => u.NetUserId == userId))
            .ToListAsync()).Select(x => Mapper.Map(x));
    }

    public SetInExerc? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<DTO.SetInExerc?> FindAsync(Guid id, Guid userId = default)
    {
        return Mapper.Map( await RepositoryDbSet
            .Include(e => e.ExerInWorkout)
                .ThenInclude(w => w!.Workout)
                    .ThenInclude(v => v!.Users)
            .Where(v => v.ExerInWorkout!.Workout!.Users.Any(u => u.NetUserId == userId))
            .FirstOrDefaultAsync(e => e.Id == id));
    }

    public void Add(SetInExerc entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public SetInExerc Update(SetInExerc entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(SetInExerc entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public async Task<DTO.SetInExerc?> FindBiggestWeight(Guid exerciseId, Guid userId)
    {
        var entity = await RepositoryDbSet
            .Include(s => s.ExerInWorkout)
                .ThenInclude(e => e!.Workout)
                    .ThenInclude(w => w!.Users)
            .Where(s => s.ExerInWorkout!.Workout!.Users.Any(u => u.NetUserId == userId))
            .OrderByDescending(s => s.Weight)
            .ThenByDescending(s => s.Reps)
            .FirstOrDefaultAsync(s => s.ExerInWorkout!.ExerciseId == exerciseId);

        return Mapper.Map(entity);
    }
}