using Application.Outbox;
using Domain.Models;

namespace Application.Abstractions.Persistence.Repositories;

public interface IWriteOfficesRepository
{
    public Task AddOfficeAsync(Office office, CancellationToken cancellationToken = default);
    public Task UpdateOfficeAsync(Office office);
    public Task DeleteOfficeAsync(Guid officeId, CancellationToken cancellationToken = default);
    public Task<Office?> GetOfficeAsync(Guid officeId, CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<OutboxMessage>> GetNotProcessedMessagesAsync(int count, CancellationToken cancellationToken = default);
    public Task SetProcessedAtRangeAsync(IEnumerable<OutboxMessage> outboxMessages);
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}