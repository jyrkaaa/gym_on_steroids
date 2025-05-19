using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class WorkoutCreate
{
    public DateTime? Date { get; set; }

    [Required]
    [MaxLength(255, ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "MaxLength")]
    public string Name { get; set; } = default!;

    [Required] public bool Public { get; set; } = false;
}