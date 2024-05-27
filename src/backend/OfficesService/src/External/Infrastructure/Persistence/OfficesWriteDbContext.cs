using Application.Outbox;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class OfficesWriteDbContext : DbContext
{
    public DbSet<Office> Offices { get; set; } = null!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;
}