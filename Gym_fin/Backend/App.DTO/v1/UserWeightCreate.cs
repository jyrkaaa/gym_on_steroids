using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class UserWeightCreate
{
    [Required]
    public decimal WeightKg { get; set; }

    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string? Desc { get; set; }

    [Required]
    public DateTime Date { get; set; }

}