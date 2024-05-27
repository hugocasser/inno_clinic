using Domain.Abstractions;

namespace Application.Outbox;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public Entity? Entity { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}