using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class ExerciseBLLMapper : IMapper<App.BLL.DTO.Exercise, App.DAL.DTO.Exercise>
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

            ExerciseCategory = entity.ExerciseCategoryId != null ? new DTO.ExerciseCategory {Id = entity.ExerciseCategoryId.Value, Name = entity.ExerciseCategory!.Name } : null,

            ExerTarget = null,
            ExerGuide = null,
            ExerInWorkouts = null
        };
    }
}