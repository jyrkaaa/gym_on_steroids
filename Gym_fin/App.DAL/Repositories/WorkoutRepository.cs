using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;

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

    public Task<IEnumerable<Workout>> AllAsync(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Workout? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<Workout?> FindAsync(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public void Add(Workout entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Workout Update(Workout entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(Workout entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}