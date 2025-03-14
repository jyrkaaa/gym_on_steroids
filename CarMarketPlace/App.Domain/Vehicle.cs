using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Vehicle : BaseEntity
{
    public Guid UserId { get; set; }
    [MaxLength(128)]
    public string Title { get; set; } = default!;
    [MaxLength(128)]

    public string? Description { get; set; }
    public decimal? Price { get; set; }  // Nullable for auction-based vehicles
    public Guid CategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsSold { get; set; } = false;
    public bool IsAuction { get; set; } = false;
    public decimal? StartingBid { get; set; }
    public DateTime? AuctionEndTime { get; set; }

    // Navigation properties
    public User User { get; set; } = default!;
    public Category Category { get; set; } = default!;
    public ICollection<VehicleImage>? Images { get; set; } = new List<VehicleImage>();
    public ICollection<Bid>? Bids { get; set; } = new List<Bid>();
    public ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
    public ICollection<Review>? Reviews { get; set; } = new List<Review>();
}