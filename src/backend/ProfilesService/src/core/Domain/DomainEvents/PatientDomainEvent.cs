using Domain.Abstractions.DomainEvents;
using Domain.Models;
using Newtonsoft.Json;

namespace Domain.DomainEvents;

public class PatientDomainEvent : IDomainEvent
{
    private readonly Patient _patient;
    private readonly EventType _eventType;

    public EventType GetEventType()
    {
        return _eventType;
    }

    public Patient GetEntity()
    {
        return _patient;
    }
    
    private PatientDomainEvent(Patient patient, EventType eventType)
    {
        _patient = patient;
        _eventType = eventType;
    }
    
    public string Serialize()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings() 
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        });
    }
    
    public static IDomainEvent Created(Patient patient)
    {
        return new PatientDomainEvent(patient, EventType.Created);
    }
    
    public static IDomainEvent Updated(Patient patient)
    {
        return new PatientDomainEvent(patient, EventType.Updated);
    }
    
    public static IDomainEvent Deleted(Patient patient)
    {
        return new PatientDomainEvent(patient, EventType.Deleted);
    }
}