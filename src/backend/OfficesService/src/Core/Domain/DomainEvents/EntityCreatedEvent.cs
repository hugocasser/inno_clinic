using Domain.Abstractions.Events;

namespace Domain.DomainEvents;

public record EntityCreatedEvent<T>(T Entity) : IDomainEvent
{
    public string GetEventType()
    {
        return "created";
    }
}