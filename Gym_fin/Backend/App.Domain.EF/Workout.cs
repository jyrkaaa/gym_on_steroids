using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.EF;

public class Workout : BaseEntity
{
    public DateTime? Date { get; set; }

    [Required]
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Name { get; set; } = default!;

    [Required] public bool Public { get; set; } = false;

    public ICollection<ExerInWorkout>? Exercises { get; set; }
    public ICollection<UsersInWorkout>? Users { get; set; }
}