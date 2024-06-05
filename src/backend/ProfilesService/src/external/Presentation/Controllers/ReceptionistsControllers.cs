using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Application.Requests.Commands.Receptionists.ChangeDoctorsStatus;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Produces("application/json")]
[Route("api/receptionists")]
public class ReceptionistsControllers(IRequestSender sender) : ApiController(sender)
{
    [HttpPut]
    public async Task<IActionResult> ChangeDoctorsStatusAsync
        ([FromBody]ChangeDoctorsStatusCommand request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.SendAsync<ChangeDoctorsStatusCommand, HttpRequestResult>(request, cancellationToken);

        return Respond(result);
    }
}