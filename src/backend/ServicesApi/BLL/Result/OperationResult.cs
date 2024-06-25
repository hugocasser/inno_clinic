namespace BLL.Result;

public class OperationResult
{
    public bool IsSuccess { get; set; }
    public object? Message { get; set; }
    public int StatusCode { get; set; }
}