namespace Application.Abstractions.Services.Saga;

public interface ITransactionsNotifierService
{
    public Task NotifyAsync(List<ITransactionResult> requiredFails, List<ITransactionResult> notRequiredFails, CancellationToken cancellationToken = default);
    public Task NotifyAsync(List<ITransactionResult> successResults, CancellationToken cancellationToken = default);
}