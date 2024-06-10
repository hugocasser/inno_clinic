using Application.Abstractions.Services.Saga;

namespace Application.Result;

public class TransactionResult : ITransactionResult
{
    public bool IsSuccess { get; }
    public object? GetContent()
    {
        throw new NotImplementedException();
    }

    public T? GetContent<T>()
    {
        throw new NotImplementedException();
    }

    public bool NeedRollback { get; set; }
    public bool IsFinished { get; set; }
}