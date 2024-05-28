using Domain.Abstractions;
using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.Events;

public class GenericDomainEvent(EventType eventType, Entity entity) : IDomainEvent<Office>
{
    private readonly EventType _eventType = eventType;

    public Office GetEntity()
    {
        return (entity as Office)!;
    }

    public string GetEventType()
    {
        return _eventType.ToString();
    }
}