using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class ExerInWorkoutCreate
{
    public string? Desc { get; set; }

    [Required]
    public Guid WorkoutId { get; set; }

    [Required]
    public Guid ExerciseId { get; set; }
    
}