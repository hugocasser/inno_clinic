using Domain.Abstractions;
using Domain.DomainEvents;

namespace Domain.Models;

public class Receptionist : Profile
{
    public Guid OfficeId { get; set; }

    public void Created()
    {
        RaiseEvent(ReceptionistDomainEvent.Created(this));
    }
    
    public void Deleted()
    {
        RaiseEvent(ReceptionistDomainEvent.Deleted(this));
    }

    public void Updated()
    {
        RaiseEvent(ReceptionistDomainEvent.Updated(this));
    }
}