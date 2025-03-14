using Base.Domain;

namespace App.Domain;

public class ForumReply : BaseEntity
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ForumPost? Post { get; set; }
    public User? User { get; set; }
}