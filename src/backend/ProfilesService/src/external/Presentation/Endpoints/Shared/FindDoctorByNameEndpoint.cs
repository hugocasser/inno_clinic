using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Application.Requests.Queries.Patients.FindDoctorByName;
using FastEndpoints;

namespace Presentation.Endpoints.Shared;

public class FindDoctorByNameEndpoint(IRequestSender sender) : Endpoint<FindDoctorByNameQuery, object>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/shared/doctors");
        AllowAnonymous();
        Validator<FindDoctorByNameQueryValidator>();
        
        base.Configure();
    }
    
    public override async Task HandleAsync(FindDoctorByNameQuery request, CancellationToken cancellationToken)
    {
        var result = await sender.SendAsync<FindDoctorByNameQuery, HttpRequestResult>(request, cancellationToken);
        
        await SendAsync(result.GetContent(), result.GetStatusCode(), cancellationToken);
    }
}