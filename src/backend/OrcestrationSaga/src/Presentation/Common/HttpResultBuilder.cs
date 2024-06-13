using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using IResult = Application.Abstractions.IResult;

namespace Presentation.Common;

public static class HttpResultBuilder
{
    public static Microsoft.AspNetCore.Http.IResult? BuildResult(IResult result)
    {
        var httpResult = new ContentResult();
        httpResult.StatusCode = result.GetStatusCode();
        httpResult.Content = JsonSerializer.Serialize(result.GetContent());
        
        return httpResult as Microsoft.AspNetCore.Http.IResult;
    }
}