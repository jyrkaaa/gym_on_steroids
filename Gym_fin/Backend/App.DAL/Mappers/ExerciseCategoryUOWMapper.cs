using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class ExerciseCategoryUOWMapper : IMapper<App.DAL.DTO.ExerciseCategory, App.Domain.EF.ExerciseCategory>
{
    public ExerciseCategory? Map(Domain.EF.ExerciseCategory? entity)
    {
        if (entity == null) return null;

        return new ExerciseCategory
        {
            Id = entity.Id,
            Name = entity.Name,
            Exercises = entity.Exercises?.Select(p => new Exercise
            {
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                Date = p.Date,
                ExerciseCategoryId = p.ExerciseCategoryId,
                ExerGuideId = p.ExerGuideId,
                ExerTargetId = p.ExerTargetId,
            }).ToList(),
        };
    }
    

    public Domain.EF.ExerciseCategory? Map(ExerciseCategory? entity)
    {
        if (entity == null) return null;

        return new Domain.EF.ExerciseCategory
        {
            Id = entity.Id,
            Name = entity.Name,
            Exercises = entity.Exercises?.Select(p => new Domain.EF.Exercise
            {
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                Date = p.Date,
                ExerciseCategoryId = p.ExerciseCategoryId,
                ExerGuideId = p.ExerGuideId,
                ExerTargetId = p.ExerTargetId,
            }).ToList(),
        };
    }
}