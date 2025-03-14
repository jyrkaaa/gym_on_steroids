using Base.Domain;

namespace App.Domain;

public class SavedListing : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User? User { get; set; }
    public Vehicle? Vehicle { get; set; }
}