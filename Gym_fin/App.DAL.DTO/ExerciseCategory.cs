using System.Collections;
using System.ComponentModel.DataAnnotations;
using App.DAL.DTO;
using Base.Contracts;
using Base.Domain;

namespace App.DAL.DTO;

public class ExerciseCategory : IDomainId
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Name { get; set; } = default!;

    public ICollection? Exercises { get; set; }
}