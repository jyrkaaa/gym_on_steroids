using Base.BLL.Contracts;

namespace App.DTO.v1.Mappers;

public class ExerciseV1Mapper : IBLLMapper<App.DTO.v1.Exercise, App.BLL.DTO.Exercise>
{
    public Exercise? Map(BLL.DTO.Exercise? entity)
    {
        if (entity == null) return null;

        return new Exercise
        {
            Id = entity.Id,
            Name = entity.Name,
            Desc = entity.Desc,
            Date = entity.Date,
            ExerTargetId = entity.ExerTargetId,
            ExerGuideId = entity.ExerGuideId,
            ExerciseCategoryId = entity.ExerciseCategoryId,
            
            ExerciseCategory = entity.ExerciseCategoryId != null ? new ExerciseCategory {Id = entity.ExerciseCategoryId.Value, Name = entity.ExerciseCategory!.Name } : null,
            ExerTarget = null,
            ExerGuide = null,
            ExerInWorkouts = null
        };
    }

    public BLL.DTO.Exercise? Map(Exercise? entity)
    {
        if (entity == null) return null;

        return new BLL.DTO.Exercise
        {
            Id = entity.Id,
            Name = entity.Name,
            Desc = entity.Desc,
            Date = entity.Date,
            ExerTargetId = entity.ExerTargetId,
            ExerGuideId = entity.ExerGuideId,
            ExerciseCategoryId = entity.ExerciseCategoryId,
            ExerciseCategory = entity.ExerciseCategoryId != null ? new BLL.DTO.ExerciseCategory {Id = entity.ExerciseCategoryId.Value, Name = entity.ExerciseCategory!.Name } : null,

            ExerTarget = null,
            ExerGuide = null,
            ExerInWorkouts = null
        };
    }

    public App.BLL.DTO.Exercise? Map(ExerciseCreate entity)
    {
        var model = new App.BLL.DTO.Exercise()
        {
            Id = Guid.NewGuid(),
            Name = entity.Name,
            Desc = entity.Desc,
            Date = entity.Date,
            ExerTargetId = entity.ExerTargetId,
            ExerGuideId = entity.ExerGuideId,
            ExerciseCategoryId = entity.ExerciseCategoryId,
            
        };
        return model;
    }
}