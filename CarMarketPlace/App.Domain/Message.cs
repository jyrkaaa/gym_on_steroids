using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Message : BaseEntity
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    [MaxLength(128)]
    public string? MessageText { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User? Sender { get; set; }
    public User? Receiver { get; set; }
}