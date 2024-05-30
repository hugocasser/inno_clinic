using Domain.Models;

namespace Domain.Abstractions.DomainEvents;

public interface IDomainEvent
{
    public EventType GetEventType();
    public object GetEntity();
}