using Domain.Abstractions;
using Domain.Abstractions.DomainEvents;

namespace Domain.DomainEvents;

public class ReceptionistDomainEvent : IDomainEvent
{
    public EventType GetEventType()
    {
        throw new NotImplementedException();
    }

    public object GetEntity()
    {
        throw new NotImplementedException();
    }
}