using Base.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.EF.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    // Navigation properties
    public ICollection<UserWeight>? UserWeights { get; set; }
    public ICollection<UsersInWorkout>? Workouts { get; set; }
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
    
}