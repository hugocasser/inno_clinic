using System.Diagnostics.CodeAnalysis;
using Application.Dtos.Requests;
using Application.Request.Commands.ChangeOfficeStatus;
using Application.Request.Commands.CreateOffice;
using Application.Request.Commands.UpdateOfficeInfo;
using Application.Request.Queries.GetOfficesByAddress;
using Application.Request.Queries.GetOfficesByNumber;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using IResult = Application.Abstractions.OperationResult.IResult;

namespace Presentation.Controllers;

[Route("offices/")]
[ExcludeFromCodeCoverage]
public class OfficesController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateOfficeAsync
        ([FromBody]CreateOfficeCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return FromOperationResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOfficeAsync
        ([FromBody] UpdateOfficeInfoCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        
        return FromOperationResult(result);
    }
    
    [HttpPut]
    [Route("{id:guid}/{status:bool}")]
    public async Task<IActionResult> UpdateOfficeAsync
        ([FromRoute] Guid id,  [FromRoute] bool status, CancellationToken cancellationToken)
    {
        var command = new ChangeOfficeStatusCommand(id, status);
        var result = await Sender.Send(command, cancellationToken);  

        return FromOperationResult(result);
    }
    
    [HttpGet]
    [Route("{getBy}/{onlyActive:bool}")]
    public async Task<IActionResult> GetOfficesAsync
        ([FromRoute]string getBy, [FromRoute]bool onlyActive, [FromBody]PageSettings pageSettings, CancellationToken cancellationToken)
    {
        IRequest<IResult> query = char.IsNumber(getBy[0]) 
            ? new GetOfficesByNumberQuery(getBy, pageSettings, onlyActive) 
            : new GetOfficesByAddressQuery(getBy, pageSettings, onlyActive);
    
        var result = await Sender.Send(query, cancellationToken);
    
        return FromOperationResult(result);
    }
}