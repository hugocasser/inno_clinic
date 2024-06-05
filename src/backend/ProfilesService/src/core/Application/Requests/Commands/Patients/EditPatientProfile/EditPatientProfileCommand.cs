using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Domain.Models;

namespace Application.Requests.Commands.Patients.EditPatientProfile;

public record EditPatientProfileCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    Guid? PhotoId,
    DateOnly BirthDate)
    : IRequest<HttpRequestResult>
{
    public void MapModel(Patient model)
    {
        model.FirstName = FirstName;
        model.LastName = LastName;

        if (!string.IsNullOrEmpty(MiddleName))
        {
            model.MiddleName = MiddleName;
        }
        
        if (PhotoId != null)
        {
            model.PhotoId = PhotoId.Value;
        }
        
        model.DateOfBirth = BirthDate;
    }
};