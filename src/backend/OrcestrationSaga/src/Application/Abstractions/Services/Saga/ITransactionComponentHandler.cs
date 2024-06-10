namespace Application.Abstractions.Services.Saga;

public interface ITransactionComponentHandler
{
    public bool RollbackRequired { get;}
    public bool NeedRollback { get;}
    public Task<ITransactionResult> HandleAsync(object request, CancellationToken cancellationToken = default);
    public Task<ITransactionResult> TryRollbackAsync(CancellationToken cancellationToken = default);
}