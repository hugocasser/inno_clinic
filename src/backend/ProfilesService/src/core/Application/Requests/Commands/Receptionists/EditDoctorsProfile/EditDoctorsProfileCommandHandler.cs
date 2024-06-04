using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Write;
using Application.Abstractions.Services.ExternalServices;
using Application.Dtos.Views;
using Application.Dtos.Views.Doctors;
using Application.Dtos.Views.Patients;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.EditDoctorsProfile;

public class EditDoctorsProfileCommandHandler(
    IWriteDoctorsRepository repository,
    IStatusesRepository statusesRepository)
    : IRequestHandler<EditDoctorsProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(EditDoctorsProfileCommand request, CancellationToken cancellationToken = default)
    {
        var doctor = await repository.GetByIdAsync(request.DoctorId, cancellationToken);
        
        if (doctor is null)
        {
            return HttpResultBuilder.BadRequest(HttpErrorMessages.ProfileNotFound);
        }
        
        var status = await statusesRepository.GetByIdAsync(request.StatusId, cancellationToken);
        
        if (status is null)
        {
            return HttpResultBuilder.BadRequest(HttpErrorMessages.StatusNotExist);
        }
        
        request.MapDoctor(doctor, status);
        doctor.Updated();
        
        await repository.UpdateAsync(doctor, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.Success(DoctorViewDto.MapFromModel(doctor));
    }
}