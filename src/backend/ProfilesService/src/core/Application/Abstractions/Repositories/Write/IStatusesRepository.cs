using Domain.Models;

namespace Application.Abstractions.Repositories.Write;

public interface IStatusesRepository
{
    public Task CreateAsync(DoctorsStatus status, CancellationToken cancellationToken = default);
    public Task<DoctorsStatus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}