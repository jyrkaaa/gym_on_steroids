using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.EF;
using App.Domain.EF.Identity;
using Base.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    // Navigation properties
    public ICollection<UserWeight>? UserWeights { get; set; }
    public ICollection<UsersInWorkout>? Workouts { get; set; }
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
    
}