using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Domain.Models;

namespace Application.Requests.Commands.Receptionists.CreatePatientsProfile;

public record CreatePatientsProfileCommand
    (string FirstName,
        string LastName,
        string MiddleName,
        DateOnly BirthDate) : IRequest<HttpRequestResult>
{
    public Patient MapToModel()
    {
        var patient = new Patient
        {
            FirstName = FirstName,
            LastName = LastName,
            MiddleName = MiddleName,
            DateOfBirth = BirthDate,
            PhotoId = Guid.Empty,
            UserId = Guid.Empty,
            IsDeleted = false,
            IsLinkedToAccount = false
        };
        
        return patient;
    }
}