using Domain.Abstractions.Events;

namespace Domain.DomainEvents;

public record EntityDeletedEvent<T>(T Entity) : IDomainEvent
{
    public string GetEventType()
    {
        return "deleted";
    }
}