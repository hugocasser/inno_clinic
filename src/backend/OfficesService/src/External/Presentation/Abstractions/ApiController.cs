using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Application.Abstractions.OperationResult.IResult;

namespace Presentation.Abstractions;

[ApiController]
[Route("api/")]
[ExcludeFromCodeCoverage]
[Produces("application/json")]
public abstract class ApiController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;

    protected static IActionResult FromOperationResult(IResult result)
    {
        return new ObjectResult(result.GetOperationResult())
        {
            StatusCode = result.GetStatusCode()
        };
    }
}