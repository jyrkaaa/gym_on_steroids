using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class ExerciseBLLMapper : IBLLMapper<App.BLL.DTO.Exercise, App.DAL.DTO.Exercise>
{
    public Exercise? Map(DTO.Exercise? entity)
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
            
            // TODO: Map nested objects if needed
            ExerciseCategory = null,
            ExerTarget = null,
            ExerGuide = null,
            ExerInWorkouts = null
        };
    }

    public DTO.Exercise? Map(Exercise? entity)
    {
        if (entity == null) return null;

        return new DTO.Exercise
        {
            Id = entity.Id,
            Name = entity.Name,
            Desc = entity.Desc,
            Date = entity.Date,
            ExerTargetId = entity.ExerTargetId,
            ExerGuideId = entity.ExerGuideId,
            ExerciseCategoryId = entity.ExerciseCategoryId,

            // TODO: Map nested objects if needed
            ExerciseCategory = null,
            ExerTarget = null,
            ExerGuide = null,
            ExerInWorkouts = null
        };
    }
}