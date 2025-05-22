using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.BLL.DTO;

public class Workout : IDomainId
{
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }
    public string? CreatedBy { get; set; }

    [Required]
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Name { get; set; } = default!;

    [Required] public bool Public { get; set; } = false;

    public ICollection<ExerInWorkout>? Exercises { get; set; }
    public ICollection<UsersInWorkout>? Users { get; set; }
}