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
        var filter = DoctorsSpecifications.OfficeNotDeleted(request.OfficeId);

        var doctors = new List<DoctorListItemViewDto>();
        
        await foreach (var doctor in repository.GetByManyAsync(filter, request.PageSettings, cancellationToken))
        {
            doctors.Add(DoctorListItemViewDto.MapFromReadModel(doctor));
        }
        
        return HttpResultBuilder.Success(doctors);
    }
}