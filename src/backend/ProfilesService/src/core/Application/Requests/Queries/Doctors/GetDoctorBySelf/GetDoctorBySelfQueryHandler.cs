using Application.Abstractions.CQRS;
using Application.Abstractions.Http;
using Application.Abstractions.Repositories.Read;
using Application.Dtos.Views.Doctors;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;
using Application.ReadModels;
using Application.Specifications;

namespace Application.Requests.Queries.Doctors.GetDoctorBySelf;

public class GetDoctorBySelfQueryHandler
    (IHttpContextAccessorExtensions httpContextAccessor,
        IReadDoctorsRepository repository)
    : IRequestHandler<GetDoctorBySelfQuery, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(GetDoctorBySelfQuery request, CancellationToken cancellationToken = default)
    {
        if (httpContextAccessor.GetUserIdFromClaims() != request.Id)
        {
            return HttpResultBuilder.Unauthorized();
        }
        
        var filter = DoctorsSpecifications.ByIdNotDeleted(request.Id);
        
        var doctor = await repository
            .GetByAsync(filter, cancellationToken);
        
        return doctor is null 
            ? HttpResultBuilder.NotFound(HttpErrorMessages.ProfileNotFound)
            : HttpResultBuilder.Success(DoctorsSelfViewDto.MapFormModel(doctor));
    }
}