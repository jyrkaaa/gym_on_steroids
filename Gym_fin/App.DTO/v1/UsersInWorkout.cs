using Base.Contracts;

namespace App.DTO.v1;

public class UsersInWorkout : IDomainId
{
    public Guid Id { get; set; }
    public Guid? NetUserId { get; set; }

    public Guid? WorkoutId { get; set; }

    public Guid? NetUser { get; set; }
    public Workout? Workout { get; set; }
}