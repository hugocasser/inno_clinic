using Application.Services.TransactionalOutbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.Configurations;

public class SerializedEventConfiguration : IEntityTypeConfiguration<SerializedEvent>
{
    public void Configure(EntityTypeBuilder<SerializedEvent> builder)
    {
        builder
            .HasKey(serializedEvent => serializedEvent.Id);
        
        builder
            .HasOne(evnt => evnt.OutboxMessage)
            .WithOne(outboxMessage => outboxMessage.SerializedDomainEvent)
            .HasForeignKey<SerializedEvent>(evnt => evnt.OutboxMessageId)
            .IsRequired();
    }
}