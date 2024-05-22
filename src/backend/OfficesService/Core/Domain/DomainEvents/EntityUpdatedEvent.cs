using Domain.Abstractions.Events;

namespace Domain.DomainEvents;

public record EntityUpdatedEvent<T>(T Entity) : IDomainEvent
{
    public string GetEventType()
    {
        return "updated";
    }
}