using Microsoft.AspNetCore.Mvc;
using IResult = Application.Abstractions.OperationResult.IResult;

namespace Presentation.Common;

public static class ResultProcessor
{
    public static IActionResult FromOperationResult(IResult result)
    {
        return new OkObjectResult(result.GetOperationResult())
        {
            StatusCode = result.GetStatusCode()
        };
    }
}