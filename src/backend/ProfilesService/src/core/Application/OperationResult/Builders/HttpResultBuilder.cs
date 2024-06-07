using Application.OperationResult.Errors;
using Application.OperationResult.Results;

namespace Application.OperationResult.Builders;

public static class HttpResultBuilder
{
    public static HttpRequestResult Success(object content)
    {
        return new HttpRequestResult(content).WithStatusCode(200);
    }

    public static HttpRequestResult NoContent()
    {
        return new HttpRequestResult(204);
    }
     
    public static HttpRequestResult Error(string message)
    {
        var error = new HttpResponseError(message);
        
        return new HttpRequestResult(error);
    }

    public static HttpRequestResult WithStatusCode(this HttpRequestResult result, int statusCode)
    {
        result.SetStatusCode(statusCode);
        
        return result;
    }
    
    public static HttpRequestResult NotFound(string message)
    {
        return Error(message).WithStatusCode(404);
    }
    
    public static HttpRequestResult BadRequest(string message)
    {
        return Error(message).WithStatusCode(400);
    }
    
    public static HttpRequestResult Unauthorized()
    {
        return Error(HttpErrorMessages.Unauthorized).WithStatusCode(401);
    }
    
    public static HttpRequestResult Forbidden(string message)
    {
        return Error(message).WithStatusCode(403);
    }
}