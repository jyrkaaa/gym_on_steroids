using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Category : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    // Navigation property
    public ICollection<Vehicle>? Vehicles { get; set; } = new List<Vehicle>();
}