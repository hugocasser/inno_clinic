using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Write;
using Application.Abstractions.Services.ExternalServices;
using Application.Dtos.Views;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;
using Domain.Models;

namespace Application.Requests.Commands.Receptionists.CreateDoctorProfile;

public class CreateDoctorProfileCommandHandler
    (IWriteDoctorsRepository repository,
        IAuthService authService,
        ISpecializationsService specializationsService,
        IOfficesService officesService,
        IPhotoService photoService,
        IStatusesRepository statusesRepository)
    : IRequestHandler<CreateDoctorProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(CreateDoctorProfileCommand request, CancellationToken cancellationToken = default)
    {
        var status = await statusesRepository.GetByIdAsync(request.StatusId, cancellationToken);

        if (status is null)
        {
            return HttpResultBuilder.BadRequest(HttpErrorMessages.StatusNotExist);
        }
        
        var specializationCheckResult = await specializationsService.CheckSpecializationAsync(request.SpecializationId, cancellationToken);

        if (!specializationCheckResult.IsSuccess)
        {
            return HttpResultBuilder.BadRequest(HttpErrorMessages.SpecializationNotFound); 
        }
        
        var officeCheckResult = await officesService.CheckOfficeAsync(request.OfficeId, cancellationToken);
        
        if (!officeCheckResult.IsSuccess)
        {
            return HttpResultBuilder.BadRequest(HttpErrorMessages.OfficeNotFound);
        }

        var doctor = request.MapToModel(status);

        doctor.Created();
        
        await repository.AddAsync(doctor, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        
        
        return HttpResultBuilder.Success(DoctorWithoutPhotoViewDto.MapFromModel(doctor));
    }
}