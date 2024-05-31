using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.Configurations;

public class DoctorsConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(doctor => 
            doctor.Id);
        
        builder
            .Property(doctor => doctor.Id)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(doctor => doctor.FirstName)
            .HasMaxLength(100)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(doctor => doctor.LastName)
            .HasMaxLength(100)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(doctor => doctor.MiddleName)
            .HasMaxLength(100)
            .ValueGeneratedNever()
            .IsRequired(false);
        
        builder
            .Property(doctor => doctor.SpecializationId)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(doctor => doctor.IsActive)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(doctor => doctor.IsDeleted)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(doctor => doctor.PhotoId)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(doctor => doctor.DateOfBirth)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(doctor => doctor.StartedCareer)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(doctor => doctor.OfficeId)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(doctor => doctor.UserId)
            .ValueGeneratedNever()
            .IsRequired();
    }
}