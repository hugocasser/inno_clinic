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
    
    public void Delete()
    {
        IsDeleted = true;
        RaiseEvent(ReceptionistDomainEvent.Deleted(this));
    }
}