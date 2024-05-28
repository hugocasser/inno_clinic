using Application.Outbox;
using Domain.Models;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class OfficesWriteDbContext : DbContext , IOfficesWriteDbContext
{
    public DbSet<Office> Offices { get; set; } = null!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;
    
    public OfficesWriteDbContext(DbContextOptions<OfficesWriteDbContext> options) : base(options) {}
    
    public OfficesWriteDbContext(): base(){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficesWriteDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}