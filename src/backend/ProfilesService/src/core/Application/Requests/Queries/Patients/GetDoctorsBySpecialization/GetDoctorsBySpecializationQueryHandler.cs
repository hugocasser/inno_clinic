using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Read;
using Application.Dtos.Views.Doctors;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;
using Application.Specifications;

namespace Application.Requests.Queries.Patients.GetDoctorsBySpecialization;

public class GetDoctorsBySpecializationQueryHandler
    (IReadDoctorsRepository repository)
    : IRequestHandler<GetDoctorsBySpecializationQuery, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(GetDoctorsBySpecializationQuery request, CancellationToken cancellationToken = default)
    {
        var filter = DoctorsSpecifications.SpecializationNotDeleted(request.Specialization);
        var doctors = new List<DoctorListItemViewDto>();
        
        await foreach (var doctor in repository.GetByManyAsync(filter, request.PageSettings, cancellationToken))
        {
            doctors.Add(DoctorListItemViewDto.MapFromReadModel(doctor));
        }
        
        return HttpResultBuilder.Success(doctors);
    }
}