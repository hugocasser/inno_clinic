using Domain.Models;

namespace Domain.Abstractions.DomainEvents;

public interface IDomainEvent
{
    public string Serialize();
};