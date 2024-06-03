using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Domain.Models;

namespace Application.Requests.Commands.Receptionists.CreateReceptionistProfile;

public record CreateReceptionistProfileCommand(
    string FirstName,
    string LastName,
    string MiddleName,
    Guid OfficeId,
    Guid UserId,
    Guid? PhotoId)
    : IRequest<HttpRequestResult>
{
    public Receptionist MapToModel()
    {
        var receptionist = new Receptionist
        {
            FirstName = FirstName,
            LastName = LastName,
            MiddleName = MiddleName,
            OfficeId = OfficeId,
            UserId = UserId,
        };
        
        if (PhotoId is not null)
        {
            receptionist.PhotoId = PhotoId.Value;
        }
        
        return receptionist;
    }
};