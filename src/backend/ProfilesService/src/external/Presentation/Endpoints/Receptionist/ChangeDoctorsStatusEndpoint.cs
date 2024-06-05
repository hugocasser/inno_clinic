using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Application.Requests.Commands.Receptionists.ChangeDoctorsStatus;
using FastEndpoints;

namespace Presentation.Endpoints.Receptionist;

public class ChangeDoctorsStatusEndpoint(IRequestSender sender) : Endpoint<ChangeDoctorsStatusCommand, object>
{
    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("api/receptionists/doctors/status");
        AllowAnonymous();
        
        base.Configure();
    }
    
    public override async Task HandleAsync(ChangeDoctorsStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.SendAsync<ChangeDoctorsStatusCommand, HttpRequestResult>(request, cancellationToken);
        
        await SendAsync(result.GetContent(), result.GetStatusCode(), cancellationToken);
    }
}