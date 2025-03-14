using Base.Domain;

namespace App.Domain;

public class Bid : BaseEntity
{
    public decimal BidAmount { get; set; }
    public DateTime BidTime { get; set; } = DateTime.UtcNow;
    public Guid VehicleId { get; set; }
    public Guid UserId { get; set; }

    // Navigation properties
    public Vehicle? Vehicle { get; set; }
    public User? User { get; set; }
}