using Domain.Abstractions;

namespace Domain.Models;

public class Doctor : Profile
{
    public DateOnly DateOfBirth { get; set; }
    public DateOnly StartedCareer { get; set; }
    public Guid OfficeId { get; set; }
    public Guid SpecializationId { get; set; }
    public bool IsActive { get; set; }
}