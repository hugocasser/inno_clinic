using Domain.Abstractions.DomainEvents;
using Domain.Models;
using Newtonsoft.Json;

namespace Domain.DomainEvents;

public class DoctorDomainEvent : IDomainEvent
{
    private readonly Doctor _doctor;
    public bool SpecializationChanged { get; private init; } = false;
    private readonly EventType _eventType;
    public EventType GetEventType()
    {
        return _eventType;
    }

    public Doctor GetEntity()
    {
        return _doctor;
    }

    private DoctorDomainEvent(Doctor doctor, EventType eventType)
    {
        _doctor = doctor;
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
    
    public static IDomainEvent Created(Doctor doctor)
    {
        return new DoctorDomainEvent(doctor, EventType.Created);
    }
    
    public static IDomainEvent Updated(Doctor doctor)
    {
        return new DoctorDomainEvent(doctor, EventType.Updated);
    }
    
    public static IDomainEvent UpdatedWithSpecialization(Doctor doctor)
    {
        var domainEvent = new DoctorDomainEvent(doctor, EventType.Updated)
        {
            SpecializationChanged = true
        };

        return domainEvent;
    }
    
    public static IDomainEvent Deleted(Doctor doctor)
    {
        return new DoctorDomainEvent(doctor, EventType.Deleted);
    }
}