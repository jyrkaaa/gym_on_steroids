using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class UsersInWorkoutRepository : BaseRepository<App.DAL.DTO.UsersInWorkout, App.Domain.EF.UsersInWorkout> , IUsersInWorkoutRepository
{
    public UsersInWorkoutRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UsersInWorkoutUOWMapper())
    {
    }

    public IEnumerable<UsersInWorkout> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<DTO.UsersInWorkout>> AllAsync(Guid userId = default)
    {
        return (await RepositoryDbSet
            .Include(u => u.Workout)
            .Include(u => u.NetUser)
            .Where(u => u.NetUser!.Id == userId).ToListAsync()).Select(w => Mapper.Map(w));
    }

    public UsersInWorkout? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<DTO.UsersInWorkout?> FindAsync(Guid id, Guid userId = default)
    {
        return Mapper.Map(await RepositoryDbSet
            .Include(u => u.Workout)
            .Include(u => u.NetUser)
            .Where(u => u.NetUser!.Id == userId)
            .FirstOrDefaultAsync(u => u.Id == id));

    }

    public void Add(DTO.UsersInWorkout entity, Guid userId = default)
    {
        RepositoryDbSet.Add(Mapper.Map(entity!));
    }

    public UsersInWorkout Update(UsersInWorkout entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(UsersInWorkout entity, Guid userId = default)
    {
        RepositoryDbSet.Remove(entity);
    }

    public Task<bool> FindByWorkoutsAsync(Guid? workoutId, Guid? exerInW, Guid userId = default, bool publicWorkouts = default)
    {
        if (!workoutId.HasValue && !exerInW.HasValue)
        {
            return Task.FromResult(false);
        }
        var query = RepositoryDbSet
            .Include(u => u.Workout)
            .ThenInclude(w => w!.Exercises)
            .AsQueryable();

        // Filter by workout ID
        if (workoutId.HasValue)
        {
            query = query.Where(u => u.WorkoutId == workoutId);
        }

        // Filter by exercise-in-workout
        if (exerInW.HasValue)
        {
            query = query.Where(u => u.Workout!.Exercises.Any(e => e.Id == exerInW));
        }

        //var result = RepositoryDbSet.FirstOrDefault(w => w.WorkoutId == workoutId && w.NetUserId == userId);
        return publicWorkouts ? query.Where(u => u.Workout!.Public == true).AnyAsync() : query.Where(u => u.NetUserId == userId).AnyAsync();
    }
}