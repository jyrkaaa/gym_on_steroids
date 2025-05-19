using System.ComponentModel.DataAnnotations;
using Base.Contracts;
using Base.Domain;

namespace App.DAL.DTO;

public class ExerGuide : IDomainId
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string? Link { get; set; } = default!;

    public ICollection<Exercise>? Exercises { get; set; }
}