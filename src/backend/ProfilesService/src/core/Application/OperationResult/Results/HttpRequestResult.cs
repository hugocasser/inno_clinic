using Application.Abstractions.OperationResult;
using Application.OperationResult.Errors;

namespace Application.OperationResult.Results;

public class HttpRequestResult : IResult
{
    public bool IsSuccess { get; private set; }
    private int StatusCode { get; set; }
    private object? Content { get; set; }
    public int GetStatusCode()
    {
        return StatusCode;
    }

    public object GetContent()
    {
        return Content ?? ResponseMessages.NoContent;
    }
    
    public HttpRequestResult(object content)
    {
        IsSuccess = true;
        Content = content;
    }
    
    public HttpRequestResult(int statusCode)
    {
        IsSuccess = true;
        StatusCode = statusCode;
    }

    public HttpRequestResult(HttpResponseError error)
    {
        IsSuccess = false;
        StatusCode = error.GetStatusCode();
        Content = error;
    }
    
    public void SetStatusCode(int statusCode)
    {
        StatusCode = statusCode;
    }
}