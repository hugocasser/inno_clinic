using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Write;
using Application.Dtos.Views;
using Application.Dtos.Views.Patients;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.CreatePatientsProfile;

public class CreatePatientsProfileCommandHandler
    (IWritePatientsRepository patientsRepository): IRequestHandler<CreatePatientsProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(CreatePatientsProfileCommand request, CancellationToken cancellationToken = default)
    {
        var patient = request.MapToModel();
        
        patient.Created();
        
        await patientsRepository.AddAsync(patient, cancellationToken);
        await patientsRepository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.Success(PatientViewDto.MapFromModel(patient));
    }
}