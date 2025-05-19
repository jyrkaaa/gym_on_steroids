using System.Collections;
using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class ExerciseCategory : IDomainId
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Name { get; set; } = default!;

    public List<Exercise>? Exercises { get; set; }
}