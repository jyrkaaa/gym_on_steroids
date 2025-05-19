using Base.Contracts;

namespace App.DTO.v1;

public class AppUser : IDomainId
{
    
    public string? Username { get; set; } = default!;

    public string? Email { get; set; }
    public Guid Id { get; set; }
}