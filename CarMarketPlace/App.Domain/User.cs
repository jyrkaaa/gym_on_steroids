using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class User : BaseEntity
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ICollection<Vehicle>? Vehicles { get; set; } = new List<Vehicle>();
    public ICollection<Bid>? Bids { get; set; } = new List<Bid>();
    [InverseProperty("Buyer")]
    public ICollection<Transaction>? TransactionsAsBuyer { get; set; } = new List<Transaction>();
    [InverseProperty("Seller")]
    public ICollection<Transaction>? TransactionsAsSeller { get; set; } = new List<Transaction>();
    [InverseProperty("Reviewer")]
    public ICollection<Review>? ReviewsGiven { get; set; } = new List<Review>();
    [InverseProperty("Seller")]
    public ICollection<Review>? ReviewsReceived { get; set; } = new List<Review>();
    [InverseProperty("Sender")]
    public ICollection<Message>? MessagesSent { get; set; } = new List<Message>();
    [InverseProperty("Receiver")]
    public ICollection<Message>? MessagesReceived { get; set; } = new List<Message>();
    public ICollection<SavedListing>? SavedListings { get; set; } = new List<SavedListing>();
}