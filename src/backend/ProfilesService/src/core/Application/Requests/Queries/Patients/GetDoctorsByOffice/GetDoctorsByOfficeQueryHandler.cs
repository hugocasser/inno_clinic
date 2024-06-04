using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Read;
using Application.Dtos.Views.Doctors;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;
using Application.Specifications;

namespace Application.Requests.Queries.Patients.GetDoctorsByOffice;

public class GetDoctorsByOfficeQueryHandler
    (IReadDoctorsRepository repository): IRequestHandler<GetDoctorsByOfficeQuery, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(GetDoctorsByOfficeQuery request, CancellationToken cancellationToken = default)
    {
        var filter = DoctorsSpecifications.ByOfficeNotDeleted(request.OfficeId);
        
        var doctors = await repository.GetByManyAsync(filter, request.PageSettings, cancellationToken);
        
        return HttpResultBuilder.Success(DoctorsListItemByPatientViewDto.MapFromReadModels(doctors));
    }
}