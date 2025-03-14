using Base.Domain;

namespace App.Domain;

public class Transaction : BaseEntity
{
    public Guid BuyerId { get; set; }
    public Guid SellerId { get; set; }
    public Guid VehicleId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    public bool IsAuctionWin { get; set; } = false;

    // Navigation properties
    public User? Buyer { get; set; } 
    public User? Seller { get; set; }
    public Vehicle? Vehicle { get; set; }
}