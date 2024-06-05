using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Application.Requests.Queries.Patients.FindDoctorByName;
using Application.Requests.Queries.Patients.GetDoctorsAsPatient;
using Application.Requests.Queries.Patients.GetDoctorsByOffice;
using Application.Requests.Queries.Patients.GetDoctorsBySpecialization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Produces("application/json")]
[Route("api/shared")]
public class SharedApiController(IRequestSender sender) : ApiController(sender)
{
    [HttpGet]
    [Route("doctors")]
    public async Task<IActionResult> FindDoctorByNameAsync
        ([FromQuery] FindDoctorByNameQuery request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.SendAsync<FindDoctorByNameQuery, HttpRequestResult>(request, cancellationToken);
        
        return Respond(result);
    }
    
    [HttpGet]
    [Route("doctors/specialization")]
    public async Task<IActionResult> GetDoctorsBySpecializationAsync
        ([FromQuery] GetDoctorsBySpecializationQuery request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.SendAsync<GetDoctorsBySpecializationQuery, HttpRequestResult>(request, cancellationToken);
        
        return Respond(result);
    }

    [HttpGet]
    [Route("doctors/offices")]
    public async Task<IActionResult> GetDoctorsByOfficesAsync([FromQuery] GetDoctorsByOfficeQuery request,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.SendAsync<GetDoctorsByOfficeQuery, HttpRequestResult>(request, cancellationToken);
        
        return Respond(result);
    }
}