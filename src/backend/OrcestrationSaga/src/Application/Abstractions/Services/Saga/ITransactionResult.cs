namespace Application.Abstractions.Services.Saga;

public interface ITransactionResult : IResult
{
    public Guid TransactionId { get; set; }
    public string ComponentName { get; set; }
}