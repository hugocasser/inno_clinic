using Application.OperationResult.Errors;

namespace Application.Abstractions.Results;

public interface IResult
{
    public bool IsSuccess { get; set; }
    public Error? Error { get; set; }
    public object? GetResultMessage();
    public int? GetStatusCode();
}