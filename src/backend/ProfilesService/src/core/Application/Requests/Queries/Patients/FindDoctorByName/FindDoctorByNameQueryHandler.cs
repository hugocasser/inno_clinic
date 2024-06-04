using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Read;
using Application.Dtos.Views.Doctors;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;
using Application.Specifications;

namespace Application.Requests.Queries.Patients.FindDoctorByName;

public class FindDoctorByNameQueryHandler
    (IReadDoctorsRepository repository): IRequestHandler<FindDoctorByNameQuery, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(FindDoctorByNameQuery request, CancellationToken cancellationToken = default)
    {
        var doctors = await repository
            .GetByManyAsync(DoctorsSpecifications
                .NameNotDeleted(request.Name), request.PageSettings, cancellationToken);

        return HttpResultBuilder.Success(DoctorListItemViewDto.MapFromReadModels(doctors));
    }
}