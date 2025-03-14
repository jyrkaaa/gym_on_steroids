using Base.Domain;

namespace App.Domain;

public class ForumPost : BaseEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = default!;
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User? User { get; set; }
    public ICollection<ForumReply>? Replies { get; set; } = new List<ForumReply>();
}