using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class ExerciseCategoryBLLMapper : IBLLMapper<App.BLL.DTO.ExerciseCategory, App.DAL.DTO.ExerciseCategory>
{
    public ExerciseCategory? Map(DTO.ExerciseCategory? entity)
    {
        if (entity == null) return null;

        return new ExerciseCategory
        {
            Id = entity.Id,
            Name = entity.Name,
            Exercises = null,
        };
    }

    public DTO.ExerciseCategory? Map(ExerciseCategory? entity)
    {
        if (entity == null) return null;

        return new DTO.ExerciseCategory
        {
            Id = entity.Id,
            Name = entity.Name,
            Exercises = null,
        };
    }

    
    
}