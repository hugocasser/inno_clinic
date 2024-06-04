using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Read;
using Application.Dtos.Views.Doctors;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;
using Application.ReadModels;
using Application.Specifications;

namespace Application.Requests.Queries.Patients.GetDoctorsAsPatient;

public class GetDoctorsAsPatientQueryHandler
    (IReadDoctorsRepository repository): IRequestHandler<GetDoctorsAsPatientQuery, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(GetDoctorsAsPatientQuery request, CancellationToken cancellationToken = default)
    {
        var filter = DoctorsSpecifications.ByPatientNotDeleted(request.MinExperience, request.MaxExperience);
            
        var doctors = await repository.GetByManyAsync(filter, request.PageSettings, cancellationToken);

        return HttpResultBuilder.Success(DoctorListItemViewDto.MapFromReadModels(doctors));
    }
}