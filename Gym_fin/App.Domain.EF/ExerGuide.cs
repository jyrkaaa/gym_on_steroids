using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.EF;

public class ExerGuide : BaseEntity
{
    [Required]
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Link { get; set; } = default!;

    public ICollection<Exercise>? Exercises { get; set; }
}