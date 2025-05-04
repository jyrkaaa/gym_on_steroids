using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain.EF;
using Base.DAL.EF;

namespace App.DAL.Repositories;

public class UserWeightRepository : BaseRepository<App.DAL.DTO.UserWeight, App.Domain.EF.UserWeight>, IUserWeightRepository
{
    public UserWeightRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UserWeightUOWMapper())
    {
    }

    public IEnumerable<UserWeight> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserWeight>> AllAsync(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public UserWeight? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserWeight?> FindAsync(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public void Add(UserWeight entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public UserWeight Update(UserWeight entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(UserWeight entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}
