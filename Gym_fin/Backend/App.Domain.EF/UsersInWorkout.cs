using App.Domain.EF.Identity;
using Base.Contracts;
using Base.Domain;

namespace App.Domain.EF;

public class UsersInWorkout : IDomainId
{
    public Guid Id { get; set; }
    public Guid? NetUserId { get; set; }

    public Guid? WorkoutId { get; set; }

    public AppUser? NetUser { get; set; }
    public Workout? Workout { get; set; }
}