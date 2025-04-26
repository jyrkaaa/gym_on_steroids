using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using Base.Contracts;
using Base.Domain;

namespace App.DAL.DTO;

public class ExerInWorkout : IDomainId
{
    public Guid Id { get; set; }
    public string? Desc { get; set; }

    [Required]
    public Guid WorkoutId { get; set; }

    [Required]
    public Guid ExerciseId { get; set; }

    public Workout? Workout { get; set; }
    public Exercise? Exercise { get; set; }
    public ICollection<SetInExerc>? Sets { get; set; }
}