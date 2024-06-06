using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Application.Requests.Queries.Patients.GetDoctorsBySpecialization;

namespace Presentation.Endpoints.Shared;

public class GetDoctorsBySpecializationEndpoint(IRequestSender sender) : Endpoint<GetDoctorsBySpecializationQuery, object>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("shared/doctors/specialization");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(GetDoctorsBySpecializationQuery request, CancellationToken cancellationToken)
    {
        var result = await sender.SendAsync<GetDoctorsBySpecializationQuery, HttpRequestResult>(request, cancellationToken);
        
        await SendAsync(result.GetContent(), result.GetStatusCode(), cancellationToken);
    }   
}