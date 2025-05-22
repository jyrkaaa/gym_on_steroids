using App.Domain;
using App.Domain.EF;
using App.Domain.EF.Identity;
using Base.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
    private readonly IUserNameResolver _userNameResolver;
    private readonly ILogger<AppDbContext> _logger;

    public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameResolver usernameResolver, ILogger<AppDbContext> logger)
        : base(options)
    {
        _userNameResolver = usernameResolver;
        _logger = logger;
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
            ;
        foreach (var entry in addedEntries)
        {
            if (entry is { Entity: IDomainMeta })
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        (entry.Entity as IDomainMeta)!.CreatedAt = DateTime.UtcNow;
                        (entry.Entity as IDomainMeta)!.CreatedBy = _userNameResolver.CurrentUserId ?? "system";
                        break;
                    case EntityState.Modified:
                        entry.Property("ChangedAt").IsModified = true;
                        entry.Property("ChangedBy").IsModified = true;
                        (entry.Entity as IDomainMeta)!.ChangedAt = DateTime.UtcNow;
                        (entry.Entity as IDomainMeta)!.ChangedBy = _userNameResolver.CurrentUserId;

                        // Prevent overwriting CreatedBy/CreatedAt on update
                        entry.Property("CreatedAt").IsModified = false;
                        entry.Property("CreatedBy").IsModified = false;
                        break;
                }
            }

        }


        return base.SaveChangesAsync(cancellationToken);

    }
    public Task<int> SaveChangesAsyncWithhoutHardcode(CancellationToken cancellationToken = new CancellationToken())
    {
        var addedEntries = ChangeTracker.Entries()
            ;
        foreach (var entry in addedEntries)
        {
            if (entry is { Entity: IDomainMeta })
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        (entry.Entity as IDomainMeta)!.CreatedAt = DateTime.UtcNow;
                        (entry.Entity as IDomainMeta)!.CreatedBy = _userNameResolver.CurrentUserId ?? "system";
                        break;
                    case EntityState.Modified:
                        entry.Property("ChangedAt").IsModified = true;
                        entry.Property("ChangedBy").IsModified = true;
                        (entry.Entity as IDomainMeta)!.ChangedAt = DateTime.UtcNow;
                        (entry.Entity as IDomainMeta)!.ChangedBy = _userNameResolver.CurrentUserId;
                        break;
                }
            }

        }


        return base.SaveChangesAsync(cancellationToken);

    }

    
    
}