using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Application.Requests.Queries.Patients.GetDoctorsAsPatient;
using FastEndpoints;

namespace Presentation.Endpoints.Patient;

public class GetDoctorsAsPatientEndpoint(IRequestSender sender) : Endpoint<GetDoctorsAsPatientQuery, object>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/patients/doctors");
        AllowAnonymous();
        Validator<GetDoctorsAsPatientQueryValidator>();
        
        base.Configure();
    }
    
    public override async Task HandleAsync(GetDoctorsAsPatientQuery request, CancellationToken cancellationToken)
    {
        var result = await sender.SendAsync<GetDoctorsAsPatientQuery, HttpRequestResult>(request, cancellationToken);
        
        await SendAsync(result.GetContent(), result.GetStatusCode(), cancellationToken);
    }
}