using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Domain.Models;

namespace Application.Requests.Commands.Receptionists.EditDoctorsProfile;

public record EditDoctorsProfileCommand(
    Guid DoctorId,
    Guid? PhotoId,
    string FirstName, 
    string LastName, 
    string? MiddleName, 
    DateOnly BirthDate, 
    Guid SpecializationId, 
    Guid OfficeId, 
    DateOnly CareerStarted, 
    Guid StatusId) : IRequest<HttpRequestResult>
{
    public void MapDoctor(Doctor doctor, DoctorsStatus status)
    {
        doctor.FirstName = FirstName;
        doctor.LastName = LastName;
        doctor.DateOfBirth = BirthDate;
        doctor.SpecializationId = SpecializationId;
        doctor.OfficeId = OfficeId;
        doctor.StartedCareer = CareerStarted;

        if (PhotoId != null)
        {
            doctor.PhotoId = PhotoId.Value;
        }

        if (!string.IsNullOrEmpty(MiddleName))
        {
            doctor.MiddleName = MiddleName;
        }
        
        doctor.StatusId = StatusId;
        doctor.Status = status;
        
        doctor.Updated();
    } 
};