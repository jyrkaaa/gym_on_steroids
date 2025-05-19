using Base.Contracts;

namespace App.DTO.v1;

public class UsersInWorkoutCreate
{
    public Guid? NetUserId { get; set; }
    public Guid? WorkoutId { get; set; }

}