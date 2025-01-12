using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Write;
using Application.Dtos.Views.Patients;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Patients.CreatePatientProfile;

public class CreatePatientProfileCommandHandler
    (IWritePatientsRepository repository): IRequestHandler<CreatePatientProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(CreatePatientProfileCommand request, CancellationToken cancellationToken = default)
    {
        var patient = request.MapToModel();
        
        patient.Created();
        
        await repository.AddAsync(patient, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.Success(PatientViewDto.MapFromModel(patient));
    }
}