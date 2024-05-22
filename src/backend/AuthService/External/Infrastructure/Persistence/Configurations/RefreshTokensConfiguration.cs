using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RefreshTokensConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new {x.UserId, x.Token}).IsUnique();
        
        builder
            .HasOne(token => token.User)
            .WithOne(user => user.RefreshToken)
            .HasForeignKey<RefreshToken>(token => token.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}