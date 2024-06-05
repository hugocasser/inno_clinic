using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Application.Requests.Queries.Patients.GetDoctorsByOffice;
using FastEndpoints;

namespace Presentation.Endpoints.Shared;

public class GetDoctorsByOfficesEndpoint(IRequestSender sender) : Endpoint<GetDoctorsByOfficeQuery, object>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/shared/doctors/offices");
        AllowAnonymous();
        Validator<GetDoctorsByOfficeQueryValidator>();
        
        base.Configure();
    }
    
    public override async Task HandleAsync(GetDoctorsByOfficeQuery request, CancellationToken cancellationToken)
    {
        var result = await sender.SendAsync<GetDoctorsByOfficeQuery, HttpRequestResult>(request, cancellationToken);
        
        await SendAsync(result.GetContent(), result.GetStatusCode(), cancellationToken);
    }
}