using App.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL;

public class AppDbContext : IdentityDbContext
{
    public new DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleImage> VehicleImages { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ForumPost> ForumPosts { get; set; }
    public DbSet<ForumReply> ForumReplies { get; set; }
    public DbSet<SavedListing> SavedListings { get; set; }

    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}