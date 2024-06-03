using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Domain.Models;

namespace Application.Requests.Commands.Patients.CreatePatientProfile;

public record CreatePatientProfileCommand
    (string FirstName,
        string LastName,
        string? MiddleName,
        DateOnly BirthDate,
        Guid UserId,
        Guid? PhotoId)
    : IRequest<HttpRequestResult>
{
    public Patient MapToModel()
    {
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            FirstName = FirstName,
            LastName = LastName,
            DateOfBirth = BirthDate,
            UserId = UserId,
            IsLinkedToAccount = true,
        };

        if (!string.IsNullOrEmpty(MiddleName))
        {
            patient.MiddleName = MiddleName;
        }
        
        if (PhotoId is not null)
        {
            patient.PhotoId = PhotoId.Value;
        }
        
        return patient;
    }
};