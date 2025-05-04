using Base.BLL.Contracts;

namespace App.DTO.v1.Mappers;

public class ExerciseCategoryV1Mapper : IBLLMapper<App.DTO.v1.ExerciseCategory, App.BLL.DTO.ExerciseCategory>
{
    public ExerciseCategory? Map(BLL.DTO.ExerciseCategory? entity)
    {
        if (entity == null) return null;

        return new ExerciseCategory
        {
            Id = entity.Id,
            Name = entity.Name,
            Exercises = null,
        };
    }

    public BLL.DTO.ExerciseCategory? Map(ExerciseCategory? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.ExerciseCategory
        {
            Id = entity.Id,
            Name = entity.Name,
            Exercises = null,
        };
    }

    
    
}