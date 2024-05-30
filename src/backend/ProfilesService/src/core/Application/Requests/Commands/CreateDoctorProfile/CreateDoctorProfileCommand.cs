using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Domain.Models;

namespace Application.Requests.Commands.CreateDoctorProfile;

public record CreateDoctorProfileCommand(
    Guid? PhotoId,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    string Email,
    Guid SpecializationId,
    Guid OfficeId,
    DateOnly CareerStarted,
    bool Status)
    : IRequest<HttpRequestResult>
{
    public Doctor MapToModel()
    {
        var doctor = new Doctor
        {
            Id = Guid.NewGuid(),
            FirstName = FirstName,
            LastName = LastName,
            DateOfBirth = BirthDate,
            SpecializationId = SpecializationId,
            OfficeId = OfficeId,
            StartedCareer = CareerStarted,
            IsActive = Status,
            IsDeleted = false
        };

        if (!string.IsNullOrEmpty(MiddleName))
        {
            doctor.MiddleName = MiddleName;
        }
        
        return doctor;
    }
};