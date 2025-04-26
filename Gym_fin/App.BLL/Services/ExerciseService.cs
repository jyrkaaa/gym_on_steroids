
using Base.BLL;
using App.BLL.Contracts;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.DAL.Contracts;
using App.DAL.DTO;


namespace App.BLL.Services;

public class ExerciseService : BaseService<App.BLL.DTO.Exercise, App.DAL.DTO.Exercise, App.DAL.Contracts.IExerciseRepository>, IExerciseService
{
    public ExerciseService(
        IAppUOW serviceUOW,
        IBLLMapper<DTO.Exercise, Exercise> bllMapper) : base(serviceUOW, serviceUOW.ExerciseRepository, bllMapper)
    {
    }
}
