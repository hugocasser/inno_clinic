using Domain.Abstractions.DomainEvents;

namespace Application.Services.TransactionalOutbox;

public class OutboxMessage
{
    public Guid Id { get;  init; }
    public SerializedEvent SerializedDomainEvent { get; init; } = null!;
    public Guid SerializedDomainEventId { get;  init; }
    public DateTime? ProcessedAt { get;  set; }
    public DateTime CreatedAt { get;  init; } = DateTime.UtcNow;

    public static OutboxMessage Create(IDomainEvent domainEvent)
    {
        var serializedDomainEvent = SerializedEvent.Create(domainEvent);
        
        var message = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            SerializedDomainEvent = serializedDomainEvent,
            SerializedDomainEventId = serializedDomainEvent.Id
        };
        
        serializedDomainEvent.SetMessage(message);
        
        return message;
    }

    public IDomainEvent? GetDomainEvent()
    {
        return SerializedDomainEvent.GetDomainEvent();
    }

    public void Processed()
    {
        ProcessedAt = DateTime.UtcNow;
    }
}