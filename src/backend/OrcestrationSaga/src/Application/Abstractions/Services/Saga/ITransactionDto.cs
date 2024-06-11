using System.Collections.Frozen;

namespace Application.Abstractions.Services.Saga;

public interface ITransactionDto
{
    public FrozenSet<string> GetHandlersKeys();
    public Guid TransactionId { get; set; }
}