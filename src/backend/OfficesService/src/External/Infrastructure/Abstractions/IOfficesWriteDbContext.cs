using Application.Outbox;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions;

public interface IOfficesWriteDbContext
{
    public DbSet<Office> Offices { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}