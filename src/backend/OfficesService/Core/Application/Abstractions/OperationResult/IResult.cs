
namespace Application.Abstractions.OperationResult;

public interface IResult
{
    public bool IsSuccess { get; }
    public object? ResultData { get; set; }
    public int StatusCode { get; set; }
    public object? GetOperationResult();
    public int GetStatusCode();
}