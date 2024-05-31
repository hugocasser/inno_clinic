using Application.Abstractions.TransactionalOutbox;
using Domain.Abstractions.DomainEvents;
using Newtonsoft.Json;

namespace Application.Services.TransactionalOutbox;

public class OutboxMessage : IOutboxMessage
{
    public Guid Id { get;  set; }
    public string SerializedDomainEvent { get; init; } = null!;
    public DateTime? ProcessedAt { get;  set; } = null;
    public DateTime CreatedAt { get;  init; } = DateTime.UtcNow;

    public static OutboxMessage Create(IDomainEvent domainEvent)
    {
        var message = new OutboxMessage
        {
            SerializedDomainEvent = domainEvent.Serialize()
        };
        
        return message;
    }

    public IDomainEvent? GetDomainEvent()
    {
        return JsonConvert.DeserializeObject<IDomainEvent>(SerializedDomainEvent);
    }

    public void Processed()
    {
        ProcessedAt = DateTime.UtcNow;
    }
}