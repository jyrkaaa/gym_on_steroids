using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain.EF;

public class UserWeight : BaseEntity
{
    [Required]
    public decimal WeightKg { get; set; }

    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string? Desc { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public Guid? NetUserId { get; set; }

    public AppUser? NetUser { get; set; }
}