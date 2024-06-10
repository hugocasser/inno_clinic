using Application.Abstractions;
using Application.Abstractions.Services.Saga;

namespace Application.Result;

public static class ResultBuilder
{
    public static IResult Success(object? content)
    {
        return new Result(true, content);
    }

    public static IResult Failure()
    {
        return new Result(false);
    }
    
    public static IResult Failure(object? content)
    {
        return content == null 
            ? new Result(false)
            : new Result(false, content);
    }

    public static IResult NoContent()
    {
        return new Result(true, ResultMessages.NoContent);
    }

    public static IResult BuildFromRollbackFails(List<ITransactionResult> requiredFails, List<ITransactionResult> notRequiredFails)
    {
        throw new NotImplementedException();
    }
    
    public static ITransactionResult TransactionNoContent()
    {
        return new TransactionResult();
    }
    
    public static ITransactionResult TransactionSuccess()
    {
        return new TransactionResult();
    }
    
    public static ITransactionResult TransactionFailed(object content)
    {
        return new TransactionResult();
    }
    
    public static ITransactionResult TransactionFailed()
    {
        return new TransactionResult();
    }
}