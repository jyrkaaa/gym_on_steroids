using Base.Contracts;

namespace Base.Domain;

public abstract class BaseEntity : IDomainId
{
    public Guid Id { get; set; }
}