using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class ExerciseCategoryBLLMapper : IMapper<App.BLL.DTO.ExerciseCategory, App.DAL.DTO.ExerciseCategory>
{
    public ExerciseCategory? Map(DTO.ExerciseCategory? entity)
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

    public DTO.ExerciseCategory? Map(ExerciseCategory? entity)
    {
        if (entity == null) return null;

        entity.Exercises ??= new List<Exercise>();;
        
        return new DTO.ExerciseCategory
        {
            Id = entity.Id,
            Name = entity.Name,
            Exercises = entity.Exercises!.Select(p => new DTO.Exercise
            {
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                Date = p.Date,
                ExerciseCategoryId = p.ExerciseCategoryId,
                ExerGuideId = p.ExerGuideId,
                ExerTargetId = p.ExerTargetId,
                CreatedBy = p.CreatedBy
            }).ToList(),
        };
    }

    
    
}