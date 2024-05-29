namespace Application.OperationResult.Errors;

public class HttpResponseError : Error
{
    private int StatusCode { get; set; } = 500;
    public HttpResponseError(string message) : base(message)
    {
    }

    public HttpResponseError(Exception exception) : base(exception)
    {
    }
    
    public int GetStatusCode()
    {
        return StatusCode;
    }
    
    public void SetStatusCode(int statusCode)
    {
        StatusCode = statusCode;
    }
}