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
            .Property(outboxMessage => outboxMessage.Id)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(outboxMessage => outboxMessage.ProcessedAt)
            .ValueGeneratedNever()
            .IsRequired(false);
        
        builder
            .Property(outboxMessage => outboxMessage.SerializedDomainEvent)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(outboxMessage => outboxMessage.CreatedAt)
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasIndex(outboxMessage => outboxMessage.ProcessedAt);
    }
}