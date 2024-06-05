using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Write;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.ChangeDoctorsStatus;

public class ChangeDoctorsStatusCommandHandler(
    IStatusesRepository statusesRepository,
    IWriteDoctorsRepository repository)
    : IRequestHandler<ChangeDoctorsStatusCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(ChangeDoctorsStatusCommand request, CancellationToken cancellationToken = default)
    {
        var status = await statusesRepository
            .GetByIdAsync(request.StatusId, cancellationToken);
        
        if (status is null)
        {
            return HttpResultBuilder
                .BadRequest(HttpErrorMessages.StatusNotExist);
        }
        
        var doctor = await repository
            .GetByIdAsync(request.DoctorId, cancellationToken);
        
        if (doctor is null)
        {
            return HttpResultBuilder
                .BadRequest(HttpErrorMessages.ProfileNotFound);
        }
        
        doctor.StatusId = request.StatusId;
        doctor.Status = status;
        
        doctor.Updated();
        
        await repository.UpdateAsync(doctor);
        await repository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.NoContent();
    }
}