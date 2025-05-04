using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class ExerciseUOWMapper : IMapper<App.DAL.DTO.Exercise, App.Domain.EF.Exercise>
{
    public Exercise? Map(Domain.EF.Exercise? entity)
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
            
            ExerciseCategory = null,
            ExerTarget = null,
            ExerGuide = null,
            ExerInWorkouts = null
        };
    }

    public Domain.EF.Exercise? Map(Exercise? entity)
    {
        if (entity == null) return null;

        return new Domain.EF.Exercise
        {
            Id = entity.Id,
            Name = entity.Name,
            Desc = entity.Desc,
            Date = entity.Date.ToUniversalTime(),
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