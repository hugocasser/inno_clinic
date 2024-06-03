using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Write;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.DeletePatientsProfile;

public class DeletePatientsProfileCommandHandler
    (IWritePatientsRepository patientsRepository)
    : IRequestHandler<DeletePatientsProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(DeletePatientsProfileCommand request, CancellationToken cancellationToken = default)
    {
        var patient = await patientsRepository.GetByIdAsync(request.PatientId, cancellationToken);
        
        if (patient == null)
        {
            return HttpResultBuilder.NotFound(HttpErrorMessages.ProfileNotFound);
        }
        
        patient.Deleted();
        
        await patientsRepository.DeleteAsync(request.PatientId, cancellationToken);
        await patientsRepository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.NoContent();
    }
}