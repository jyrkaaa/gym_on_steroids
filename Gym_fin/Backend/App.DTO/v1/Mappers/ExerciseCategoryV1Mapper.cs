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
            Exercises = entity.Exercises?.Select(p => new Exercise
            {
                Id = p!.Id,
                Name = p.Name,
                Desc = p.Desc,
                Date = p.Date,
                ExerciseCategoryId = p.ExerciseCategoryId,
                ExerGuideId = p.ExerGuideId,
                ExerTargetId = p.ExerTargetId,
            }).ToList(),
        };
    }

    public BLL.DTO.ExerciseCategory? Map(ExerciseCategory? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.ExerciseCategory
        {
            Id = entity.Id,
            Name = entity.Name,
            Exercises = entity.Exercises?.Select(p => new BLL.DTO.Exercise
            {
                Id = p!.Id,
                Name = p.Name,
                Desc = p.Desc,
                Date = p.Date,
                ExerciseCategoryId = p.ExerciseCategoryId,
                ExerGuideId = p.ExerGuideId,
                ExerTargetId = p.ExerTargetId,
            }).ToList(),
        };
    }

    public BLL.DTO.ExerciseCategory? Map(ExerciseCategoryCreate? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.ExerciseCategory()
        {
            Id = Guid.NewGuid(),
            Name = entity.Name,
        };
    }

    
    
}