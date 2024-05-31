using Application.Abstractions.TransactionalOutbox;
using Domain.Abstractions.DomainEvents;
using Newtonsoft.Json;

namespace Application.Services.TransactionalOutbox;

public class OutboxMessage : IOutboxMessage
{
    public Guid Id { get;  set; }
    public string SerializedDomainEvent { get;  set; } = string.Empty;
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
    public DateTime? ProcessedAt { get;  set; } = null;
    
    public void Processed()
    {
        ProcessedAt = DateTime.UtcNow;
    }
}