using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.Configurations;

public class ReceptionistConfiguration : IEntityTypeConfiguration<Receptionist>
{
    public void Configure(EntityTypeBuilder<Receptionist> builder)
    {
        builder
            .HasKey(receptionist => receptionist.Id);
        
        builder
            .Property(receptionist => receptionist.FirstName)
            .HasMaxLength(100)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(receptionist => receptionist.LastName)
            .HasMaxLength(100)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(receptionist => receptionist.MiddleName)
            .HasMaxLength(100)
            .ValueGeneratedNever()
            .IsRequired(false);
        
        builder
            .Property(receptionist => receptionist.OfficeId)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(receptionist => receptionist.UserId)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(receptionist => receptionist.IsDeleted)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(receptionist => receptionist.PhotoId)
            .ValueGeneratedNever()
            .IsRequired();
    }
}