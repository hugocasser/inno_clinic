namespace DataAccess.OperationResult.Abstractions;

public interface IResult
{
    public bool IsSuccess { get; }
    public object? GetContent();
    public T GetTypedContent<T>();
}