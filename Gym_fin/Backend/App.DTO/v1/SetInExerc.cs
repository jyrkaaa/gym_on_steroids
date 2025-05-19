using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class SetInExerc : IDomainId
{
    public Guid Id { get; set; }
    [Required]
    public decimal Weight { get; set; }
    [Required]
    public int Reps { get; set; }

    [Required]
    public Guid ExerInWorkoutId { get; set; }

    public ExerInWorkout? ExerInWorkout { get; set; }
}