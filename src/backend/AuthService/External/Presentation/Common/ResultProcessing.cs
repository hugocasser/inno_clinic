using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IResult = Application.Abstractions.Results.IResult;

namespace Presentation.Common;

public static class ResultProcessing
{
    public static IActionResult FromResult(IResult result)
    {
        return new ObjectResult(result.GetResultMessage())
        {
            StatusCode = result.GetStatusCode()
        };
    }
}