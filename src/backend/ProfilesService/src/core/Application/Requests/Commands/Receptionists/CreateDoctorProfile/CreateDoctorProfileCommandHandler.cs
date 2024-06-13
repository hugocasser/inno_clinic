using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Write;
using Application.Dtos.Views.Doctors;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.CreateDoctorProfile;

public class CreateDoctorProfileCommandHandler
    (IWriteDoctorsRepository repository,
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

        var doctor = request.MapToModel(status);

        doctor.Created();
        
        await repository.AddAsync(doctor, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.Success(DoctorViewDto.MapFromModel(doctor));
    }
}