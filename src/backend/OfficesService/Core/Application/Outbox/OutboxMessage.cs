using Domain.Abstractions;

namespace Application.Outbox;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public Entity? Entity { get; set; }
    public DateTimeOffset ProcessedAt { get; set; }
}