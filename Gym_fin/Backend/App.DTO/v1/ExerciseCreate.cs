using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class ExerciseCreate
{
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Name { get; set; } = default!;
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string? Desc { get; set; }
    
    public DateTimeOffset Date { get; set; }

    public Guid? ExerTargetId { get; set; }

    public Guid? ExerGuideId { get; set; }

    public Guid? ExerciseCategoryId { get; set; }
}