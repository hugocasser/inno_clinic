using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Read;
using Application.Dtos.Views.Doctors;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;
using Application.Specifications;

namespace Application.Requests.Queries.Receptionists.GetDoctorByReceptionist;

public class GetDoctorByReceptionistQueryHandler
    (IReadDoctorsRepository repository): IRequestHandler<GetDoctorByReceptionistQuery, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(GetDoctorByReceptionistQuery request, CancellationToken cancellationToken = default)
    {
        var doctor = await repository.GetByAsync(DoctorsSpecifications.ByIdNotDeleted(request.Id), cancellationToken);
        
        return doctor is null
            ? HttpResultBuilder.NotFound(HttpErrorMessages.ProfileNotFound)
            : HttpResultBuilder.Success(DoctorByReceptionistViewDto.MapFormModel(doctor));
    }
}