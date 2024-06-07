using Application.Services.TransactionalOutbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.Configurations;

public class OutboxMessagesConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder
            .HasKey(outboxMessage => outboxMessage.Id);
        
        builder
            .Property(outboxMessage => outboxMessage.ProcessedAt)
            .ValueGeneratedNever()
            .IsRequired(false);
        
        builder
            .Property(outboxMessage => outboxMessage.CreatedAt)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .HasOne(outboxMessage => outboxMessage.SerializedDomainEvent)
            .WithOne(serializedDomainEvent => serializedDomainEvent.OutboxMessage)
            .HasForeignKey<OutboxMessage>(outboxMessage => outboxMessage.SerializedDomainEventId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(outboxMessage => outboxMessage.ProcessedAt);
    }
}