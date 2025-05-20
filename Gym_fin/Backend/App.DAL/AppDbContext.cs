using App.Domain;
using App.Domain.EF;
using App.Domain.EF.Identity;
using Base.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>

{
    public DbSet<ExerInWorkout> ExerInWorkout { get; set; } = default!;
    public DbSet<ExerTarget> ExerTarget { get; set; } = default!;
    public DbSet<ExerGuide> ExerGuide { get; set; } = default!;
    public DbSet<SetInExerc> SetInExerc { get; set; } = default!;
    public DbSet<UsersInWorkout> UsersInWorkout { get; set; } = default!;
    public DbSet<Workout> Workout { get; set; } = default!;
    public DbSet<ExerciseCategory> ExerciseCategory { get; set; } = default!;
    public DbSet<UserWeight> UserWeight { get; set; } = default!;
    public DbSet<Exercise> Exercise { get; set; } = default!;
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        

        builder.Entity<Workout>()
            .HasMany(w => w.Exercises)
            .WithOne(e => e.Workout)
            .HasForeignKey(e => e.WorkoutId);

        builder.Entity<ExerInWorkout>()
            .HasMany(e => e.Sets)
            .WithOne(s => s.ExerInWorkout)
            .HasForeignKey(s => s.ExerInWorkoutId);
        
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var addedEntries = ChangeTracker.Entries()
            .Where(e => e is { Entity: IDomainMeta });
        foreach (var entry in addedEntries)
        {
            if (entry.State == EntityState.Added)
            {
                (entry.Entity as IDomainMeta)!.CreatedAt = DateTime.UtcNow;
                (entry.Entity as IDomainMeta)!.CreatedBy = "system";
            }
            else if (entry.State == EntityState.Modified)
            {
                (entry.Entity as IDomainMeta)!.ChangedAt = DateTime.UtcNow;
                (entry.Entity as IDomainMeta)!.ChangedBy = "system";
                
                // Prevent overwriting CreatedBy/CreatedAt/UserId on update
                entry.Property("CreatedAt").IsModified = false;
                entry.Property("CreatedBy").IsModified = false;

                //entry.Property("UserId").IsModified = false;  all properties dont have this
            }
        }


        return base.SaveChangesAsync(cancellationToken);
    }

    
    
}