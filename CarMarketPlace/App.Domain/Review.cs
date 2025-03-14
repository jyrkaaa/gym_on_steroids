using Base.Domain;

namespace App.Domain;

public class Review : BaseEntity
{
    public Guid ReviewerId { get; set; }
    public Guid SellerId { get; set; }
    public Guid VehicleId { get; set; }
    public int Rating { get; set; }  // Range 1-5
    public string? Comment { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User? Reviewer { get; set; }
    public User? Seller { get; set; } 
    public Vehicle? Vehicle { get; set; }
}