using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.EF;

public class SetInExerc : BaseEntity
{
    [Required]
    public decimal Weight { get; set; }

    [Required]
    public Guid ExerInWorkoutId { get; set; }

    public ExerInWorkout? ExerInWorkout { get; set; }
}