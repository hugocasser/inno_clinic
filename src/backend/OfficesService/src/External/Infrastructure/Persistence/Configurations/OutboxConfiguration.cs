using Application.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OutboxConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder
            .ToTable("OutboxMessages")
            .HasKey(outboxMessage => outboxMessage.Id);
        
        builder
            .Property(outboxMessage => outboxMessage.Id)
            .ValueGeneratedNever();
        
        builder
            .Property(outboxMessage => outboxMessage.ProcessedAt)
            .ValueGeneratedNever()
            .IsRequired(false);
        
        builder
            .Property(outboxMessage => outboxMessage.CreatedAt)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(outboxMessage => outboxMessage.SerializedEvent)
            .ValueGeneratedNever()
            .IsRequired();
    }
}