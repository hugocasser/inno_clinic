using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Application.Requests.Queries.Patients.GetDoctorsAsPatient;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Produces("application/json")]
[Route("api/patients")]
public class PatientsController(IRequestSender sender) : ApiController(sender)
{
    [HttpGet]
    [Route("doctors")]
    public async Task<IActionResult> GetDoctorsAsPatient([FromQuery] GetDoctorsAsPatientQuery request,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.SendAsync<GetDoctorsAsPatientQuery, HttpRequestResult>(request, cancellationToken);
        
        return Respond(result);
    }
}