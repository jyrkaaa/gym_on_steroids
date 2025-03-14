using Base.Domain;

namespace App.Domain;

public class VehicleImage : BaseEntity
{
    public Guid VehicleId { get; set; }
    public string ImageUrl { get; set; } = default!;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Vehicle Vehicle { get; set; } = default!;
}