using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Read;
using Application.Abstractions.Repositories.Write;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.RollbackPatientsProfile;

public class RollbackPatientsProfileCommandHandler
    (IWritePatientsRepository patientsWriteRepository, IReadPatientsRepository patientsReadRepository)
    : IRequestHandler<RollbackPatientsProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(RollbackPatientsProfileCommand request, CancellationToken cancellationToken = default)
    {
        var patientFromWriteRepository = await patientsWriteRepository
            .GetByIdAsync(request.PatientId, cancellationToken);
        
        if (patientFromWriteRepository == null)
        {
            return HttpResultBuilder.NotFound(HttpErrorMessages.ProfileNotFound);
        }
        
        var patientFromReadRepository = await patientsReadRepository
            .GetByIdAsync(request.PatientId, cancellationToken);
        
        if (patientFromReadRepository == null)
        {
            return HttpResultBuilder.NotFound(HttpErrorMessages.ProfileNotFound);
        }
        
        patientFromWriteRepository.IsDeleted = false;
        patientFromReadRepository.IsDeleted = false;
        
        await patientsWriteRepository.UpdateAsync(patientFromWriteRepository, cancellationToken);
        await patientsReadRepository.UpdateAsync(patientFromReadRepository, cancellationToken);
        await patientsWriteRepository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.NoContent();
    }
}