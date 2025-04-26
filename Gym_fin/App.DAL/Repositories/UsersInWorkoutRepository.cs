using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class UsersInWorkoutRepository : BaseRepository<App.DAL.DTO.UsersInWorkout, App.Domain.EF.UsersInWorkout> , IUsersInWorkoutRepository
{
    public UsersInWorkoutRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UsersInWorkoutMapper())
    {
    }

    public IEnumerable<UsersInWorkout> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UsersInWorkout>> AllAsync(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public UsersInWorkout? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<UsersInWorkout?> FindAsync(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public void Add(UsersInWorkout entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public UsersInWorkout Update(UsersInWorkout entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(UsersInWorkout entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}