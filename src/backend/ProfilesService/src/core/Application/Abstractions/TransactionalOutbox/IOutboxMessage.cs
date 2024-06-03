using Application.Services.TransactionalOutbox;
using Domain.Abstractions.DomainEvents;

namespace Application.Abstractions.TransactionalOutbox;

public interface IOutboxMessage
{
    public IDomainEvent? GetDomainEvent();
    protected DateTime? ProcessedAt { get;  set; }
    public void Processed();
}