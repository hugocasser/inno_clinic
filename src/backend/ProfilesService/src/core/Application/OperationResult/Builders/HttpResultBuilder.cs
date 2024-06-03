using Application.OperationResult.Errors;
using Application.OperationResult.Results;

namespace Application.OperationResult.Builders;

public static class HttpResultBuilder
{
    public static HttpRequestResult Success(object content)
    {
        return new HttpRequestResult(content);
    }

    public static HttpRequestResult NoContent()
    {
        return new HttpRequestResult(204);
    }
    
    public static HttpRequestResult Error(HttpResponseError error)
    {
        return new HttpRequestResult(error);
    }

    public static HttpRequestResult Error(Exception exception)
    {
        return new HttpRequestResult(exception);
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
    
    public static HttpRequestResult InternalServerError()
    {
        return Error(HttpErrorMessages.InternalServerError).WithStatusCode(500);
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
    
    public static string FromErrors(params Error[]errors)
    {
        return string.Join(", ", errors.Select(x => x.Message));
    }
}