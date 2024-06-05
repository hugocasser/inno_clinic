using Application.Abstractions.CQRS;
using Application.OperationResult.Results;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Abstractions;

public abstract class ApiController(IRequestSender sender) : ControllerBase
{
    protected readonly IRequestSender Sender = sender;

    public ObjectResult Respond(HttpRequestResult result)
    {
        return new ObjectResult(result.GetContent())
        {
            StatusCode = result.GetStatusCode()
        };
    }
}