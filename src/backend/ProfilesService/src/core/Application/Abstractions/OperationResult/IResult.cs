namespace Application.Abstractions.OperationResult;

public interface IResult
{
    public bool IsSuccess { get; }
    public object? GetContent();
}