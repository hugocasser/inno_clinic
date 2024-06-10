namespace Application.Abstractions.Services.Saga;

public interface IFailsNotifierService
{
    public Task NotifyAsync(List<ITransactionResult> requiredFails, List<ITransactionResult> notRequiredFails, CancellationToken cancellationToken = default);
}