using Domain.Abstractions.DomainEvents;
using Domain.Models;
using Newtonsoft.Json;

namespace Domain.DomainEvents;

public class ReceptionistDomainEvent : IDomainEvent
{
    private readonly Receptionist _receptionist;
    private readonly EventType _eventType;
    
    public EventType GetEventType()
    {
        return _eventType;
    }

    public Receptionist GetEntity()
    {
        return _receptionist;
    }

    private ReceptionistDomainEvent(Receptionist receptionist, EventType eventType)
    {
        _receptionist = receptionist;
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
    
    public static IDomainEvent Created(Receptionist receptionist)
    {
        return new ReceptionistDomainEvent(receptionist, EventType.Created);
    }
    
    public static IDomainEvent Updated(Receptionist receptionist)
    {
        return new ReceptionistDomainEvent(receptionist, EventType.Updated);
    }
    
    public static IDomainEvent Deleted(Receptionist receptionist)
    {
        return new ReceptionistDomainEvent(receptionist, EventType.Deleted);
    }
}