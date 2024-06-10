namespace Application.Abstractions;

public interface IResult
{
    public bool IsSuccess { get; }
    public object? GetContent();
    public T? GetContent<T>();
}