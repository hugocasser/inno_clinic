using System.Collections.Frozen;

namespace Application.Abstractions.Services.Saga;

public interface ITransactionsHandlerService
{
    public Task<IResult> StartExecuteAsync(ITransactionDto request, CancellationToken cancellationToken = default);

    public Task<IResult> TryRollbackAsync(List<ITransactionComponentHandler> handlers,
        CancellationToken cancellationToken = default);
}