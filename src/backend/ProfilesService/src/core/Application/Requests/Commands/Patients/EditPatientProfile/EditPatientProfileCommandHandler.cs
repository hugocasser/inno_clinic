using Application.Abstractions.CQRS;
using Application.Abstractions.Http;
using Application.Abstractions.Repositories.Write;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Patients.EditPatientProfile;

public class EditPatientProfileCommandHandler
    (IHttpContextAccessorExtensions httpContextAccessor,
        IWritePatientsRepository repository)
    : IRequestHandler<EditPatientProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(EditPatientProfileCommand request, CancellationToken cancellationToken = default)
    {
        if (httpContextAccessor.CheckUserRoles())
        {
            if (request.Id != httpContextAccessor.GetUserIdFromClaims())
            {
                return HttpResultBuilder.Unauthorized();
            }
        }
        
        var patient = await repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (patient is null)
        {
            return HttpResultBuilder.NotFound(HttpErrorMessages.ProfileNotFound);
        }
        
        request.MapModel(patient);
        
        await repository.UpdateAsync(patient, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.NoContent();
    }
}