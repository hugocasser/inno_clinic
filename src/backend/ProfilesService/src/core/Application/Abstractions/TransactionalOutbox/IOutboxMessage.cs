using Application.Services.TransactionalOutbox;
using Domain.Abstractions.DomainEvents;

namespace Application.Abstractions.TransactionalOutbox;

public interface IOutboxMessage
{
    public static  OutboxMessage Create(IDomainEvent domainEvent)
    {
        var message = new OutboxMessage
        {
            SerializedDomainEvent = domainEvent.Serialize()
        };
        
        return message;
    }
    public IDomainEvent? GetDomainEvent();
    protected DateTime? ProcessedAt { get;  set; }
    public void Processed();
}