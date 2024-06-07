using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder
            .HasKey(patient => patient.Id);
        
        builder
            .Property(patient => patient.FirstName)
            .HasMaxLength(100)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(patient => patient.LastName)
            .HasMaxLength(100)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(patient => patient.MiddleName)
            .HasMaxLength(100)
            .ValueGeneratedNever()
            .IsRequired(false);
        
        builder
            .Property(patient => patient.DateOfBirth)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(patient => patient.PhotoId)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(patient => patient.IsDeleted)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(patient => patient.IsLinkedToAccount)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(patient => patient.UserId)
            .ValueGeneratedNever()
            .IsRequired();
    }
}