using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class UserWeight : IDomainId
{
    public Guid Id { get; set; }
    [Required]
    public decimal WeightKg { get; set; }

    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string? Desc { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public Guid? NetUserId { get; set; }

    public Guid? NetUser { get; set; }
}