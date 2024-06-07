using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Domain.Models;

namespace Application.Requests.Commands.Receptionists.CreateDoctorProfile;

public record CreateDoctorProfileCommand(
    Guid? PhotoId,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    Guid UserId,
    Guid SpecializationId,
    Guid OfficeId,
    DateOnly CareerStarted,
    Guid StatusId)
    : IRequest<HttpRequestResult>
{
    public Doctor MapToModel(DoctorsStatus status)
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
            IsDeleted = false,
            UserId = UserId,
            StatusId = status.Id,
            Status = status
        };

        if (!string.IsNullOrEmpty(MiddleName))
        {
            doctor.MiddleName = MiddleName;
        }
        
        if (PhotoId is not null)
        {
            doctor.PhotoId = PhotoId.Value;
        }
        
        return doctor;
    }
};