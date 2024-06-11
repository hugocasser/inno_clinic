using IResult = Application.Abstractions.IResult;

namespace Presentation.Common;

public static class HttpResultBuilder
{
    public static Microsoft.AspNetCore.Http.IResult BuildResult(IResult result)
    {
        return Results.Ok();
    }
}