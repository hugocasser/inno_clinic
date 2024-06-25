namespace Application.Abstractions;

public interface IResult
{
    public bool IsSuccess { get; init; }

    public void SetResultData<T>(T data) where T : class;

    public T? GetResultData<T>() where T : class ;
    
    public object? GetResultData();

    public void SetResultData(object data);
}