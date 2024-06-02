using Domain.Abstractions;
using Domain.DomainEvents;

namespace Domain.Models;

public class Doctor : Profile
{
    public DateOnly DateOfBirth { get; set; }
    public DateOnly StartedCareer { get; set; }
    public Guid OfficeId { get; set; }
    public Guid SpecializationId { get; set; }
    
    public DoctorsStatus Status { get; set; }
    public Guid StatusId { get; set; }

    public Doctor()
    {
        Created();
    }

    public void Deleted()
    {
        RaiseEvent(DoctorDomainEvent.Deleted(this));
    }
    
    public void ChangeSpecialization(Guid specializationId)
    {
        SpecializationId = specializationId;
        var doctorEvent = DoctorDomainEvent.UpdatedWithSpecialization(this);
        RaiseEvent(doctorEvent);
    }

    private void Created()
    {
        RaiseEvent(DoctorDomainEvent.Created(this));
    }

    public void Updated()
    {
        RaiseEvent(DoctorDomainEvent.Updated(this));
    }
}