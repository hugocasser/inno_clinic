using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Write;
using Application.Abstractions.Services.ExternalServices;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.EditDoctorsProfile;

public class EditDoctorsProfileCommandHandler(
    IWriteDoctorsRepository repository,
    IStatusesRepository statusesRepository,
    IOfficesService officesService,
    IPhotoService photoService)
    : IRequestHandler<EditDoctorsProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(EditDoctorsProfileCommand request, CancellationToken cancellationToken = default)
    {
        var doctor = await repository.GetByIdAsync(request.DoctorId, cancellationToken);
        
        if (doctor is null)
        {
            return HttpResultBuilder
                .BadRequest(HttpErrorMessages.ProfileNotFound);
        }
        
        var status = await statusesRepository
            .GetByIdAsync(request.StatusId, cancellationToken);
        
        if (status is null)
        {
            return HttpResultBuilder.BadRequest(HttpErrorMessages.StatusNotExist);
        }
        
        var officeCheckResult = await officesService
            .CheckOfficeAsync(request.OfficeId, cancellationToken);
        
        if (!officeCheckResult.IsSuccess)
        {
            return HttpResultBuilder.BadRequest(HttpErrorMessages.OfficeNotFound);
        }
        
        if (request.PhotoId is not null)
        {
            var photoCheckResult = await photoService
                .CheckPhotoAsync(request.PhotoId, cancellationToken);
            
            if (!photoCheckResult.IsSuccess)
            {
                return HttpResultBuilder.BadRequest(HttpErrorMessages.PhotoNotFound);
            }
        }
        
        request.MapDoctor(doctor, status);
        doctor.Updated();
        
        await repository.UpdateAsync(doctor, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.NoContent();
    }
}