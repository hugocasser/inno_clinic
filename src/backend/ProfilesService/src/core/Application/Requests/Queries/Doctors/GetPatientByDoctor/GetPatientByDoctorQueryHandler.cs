using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Read;
using Application.Dtos.Views.Patients;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;
using Application.Specifications;

namespace Application.Requests.Queries.Doctors.GetPatientByDoctor;

public class GetPatientByDoctorQueryHandler
    (IReadPatientsRepository repository)
    : IRequestHandler<GetPatientByDoctorQuery, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(GetPatientByDoctorQuery request, CancellationToken cancellationToken = default)
    {
        var filter = PatientsSpecifications.ByIdNotDeleted(request.Id);
        
        var patient = await repository.GetByAsync(filter, cancellationToken);
        
        return patient is null
            ? HttpResultBuilder.NotFound(HttpErrorMessages.ProfileNotFound)
            : HttpResultBuilder.Success(PatientByDoctorViewDto.MapFromModel(patient));
    }
}