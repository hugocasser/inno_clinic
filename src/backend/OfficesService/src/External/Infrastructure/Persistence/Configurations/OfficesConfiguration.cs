using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OfficesConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder
            .ToTable("Offices")
            .HasKey(office => office.Id);

        builder
            .Property(office => office.Id)
            .ValueGeneratedNever();

        builder
            .Property(office => office.Address)
            .IsRequired()
            .HasMaxLength(300)
            .ValueGeneratedNever();
        
        builder
            .Property(office => office.RegistryPhoneNumber)
            .IsRequired()
            .HasMaxLength(50)
            .ValueGeneratedNever();

        builder
            .Property(office => office.IsActive)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(office => office.PhotoId)
            .ValueGeneratedNever()
            .IsRequired(false);
    }
}