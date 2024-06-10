using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Persistence.Configurations;

public class BlobFileInfoConfiguration : IEntityTypeConfiguration<BlobFileInfo>
{
    public void Configure(EntityTypeBuilder<BlobFileInfo> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(x => x.UploadedDate)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder
            .Property(x => x.ContentType)
            .ValueGeneratedNever()
            .HasMaxLength(256)
            .IsRequired();
        
        builder
            .Property(x => x.Size)
            .ValueGeneratedNever()
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .Property(x => x.IsDeleted)
            .ValueGeneratedNever()
            .HasDefaultValue(false)
            .IsRequired();
        
        builder
            .Property(x => x.IsDeletedFromBlobStorage)
            .ValueGeneratedNever()
            .HasDefaultValue(false)
            .IsRequired();
    }
}