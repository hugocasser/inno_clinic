using System.Reflection;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Persistence;

public class FileInfoDbContext : DbContext
{
    public DbSet<BlobFileInfo> BlobFileInfos { get; set; } = null!;
    
    public FileInfoDbContext()
    {
    }
    public FileInfoDbContext(DbContextOptions<FileInfoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
    }
}