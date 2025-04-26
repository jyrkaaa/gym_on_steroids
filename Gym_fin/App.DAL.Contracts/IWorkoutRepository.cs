using App.Domain.EF;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IWorkoutRepository : IBaseRepository<App.DAL.DTO.Workout>
{
    
}