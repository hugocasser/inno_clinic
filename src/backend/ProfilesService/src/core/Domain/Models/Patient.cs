using Domain.Abstractions;
using Domain.DomainEvents;

namespace Domain.Models;

public class Patient : Profile
{
    public bool IsLinkedToAccount { get; set; }
    public DateOnly DateOfBirth { get; set; }


    public Patient()
    {
        Created();
    }
    private void Created()
    {
        RaiseEvent(PatientDomainEvent.Created(this));
    }
    
    public void Deleted()
    {
        RaiseEvent(PatientDomainEvent.Deleted(this));
    }
    
    private void Updated()
    {
        RaiseEvent(PatientDomainEvent.Updated(this));
    }
}