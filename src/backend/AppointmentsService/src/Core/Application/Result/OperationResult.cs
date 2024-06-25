namespace Application.Result;

public class OperationResult
{
    public bool IsSuccess { get; set; }
    public object? Message { get; set; }
    public int StatusCode { get; set; }

    public OperationResult(bool isSuccess = true, object? message = null, int statusCode = 204)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}