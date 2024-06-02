using Domain.Abstractions;
using Domain.DomainEvents;

namespace Domain.Models;

public class Receptionist : Profile
{
    public Guid OfficeId { get; set; }
    
    public Receptionist()
    {
        Created();
    }
    
    private void Created()
    {
        RaiseEvent(ReceptionistDomainEvent.Created(this));
    }
    
    public void Deleted()
    {
        RaiseEvent(ReceptionistDomainEvent.Deleted(this));
    }
    
    private void Updated()
    {
        RaiseEvent(ReceptionistDomainEvent.Updated(this));
    }
}