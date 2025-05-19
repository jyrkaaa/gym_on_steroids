using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.EF;

public class Exercise : BaseEntity
{
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Name { get; set; } = default!;
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string? Desc { get; set; }
    
    public DateTimeOffset Date { get; set; }

    public Guid? ExerTargetId { get; set; }

    public Guid? ExerGuideId { get; set; }

    public Guid? ExerciseCategoryId { get; set; }

    public ExerciseCategory? ExerciseCategory { get; set; }
    public ExerTarget? ExerTarget { get; set; }
    public ExerGuide? ExerGuide { get; set; }
    public ICollection<ExerInWorkout>? ExerInWorkouts { get; set; }
}