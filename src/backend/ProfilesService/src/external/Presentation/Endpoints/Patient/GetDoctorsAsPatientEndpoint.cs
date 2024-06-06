using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Application.Requests.Queries.Patients.GetDoctorsAsPatient;

namespace Presentation.Endpoints.Patient;

public class GetDoctorsAsPatientEndpoint(IRequestSender sender) : Endpoint<GetDoctorsAsPatientQuery, object>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("patients/doctors");
        AllowAnonymous();
    }
    public override async Task HandleAsync(GetDoctorsAsPatientQuery request, CancellationToken cancellationToken)
    {
        var result = await sender.SendAsync<GetDoctorsAsPatientQuery, HttpRequestResult>(request, cancellationToken);

        var code = result.GetStatusCode();
        await SendAsync(result.GetContent(), code, cancellationToken);
    }
}