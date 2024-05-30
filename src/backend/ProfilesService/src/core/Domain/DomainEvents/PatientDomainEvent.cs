using Domain.Abstractions;
using Domain.Abstractions.DomainEvents;
using Domain.Models;

namespace Domain.DomainEvents;

public class PatientDomainEvent : IDomainEvent
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