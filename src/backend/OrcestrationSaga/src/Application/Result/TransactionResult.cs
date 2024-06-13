using Application.Abstractions.Services.Saga;

namespace Application.Result;

public class TransactionResult : ITransactionResult
{
    public bool IsSuccess { get; set; }
    public object Content { get; set; }
    public object? GetContent()
    {
        return Content;
    }

    public T? GetContent<T>()
    {
        return object.Equals(Content, null) ? default : (T) Content;
    }

    public int GetStatusCode()
    {
        return IsSuccess 
            ? 200 
            : 500;
    }

    public Guid TransactionId { get; set; }
    public string ComponentName { get; set; }
}