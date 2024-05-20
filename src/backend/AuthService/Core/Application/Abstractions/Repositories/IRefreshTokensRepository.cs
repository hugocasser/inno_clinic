using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IRefreshTokensRepository
{
    public Task CreateTokenAsync(RefreshToken token, CancellationToken cancellationToken);
    public Task<RefreshToken?> GetUserRefreshTokenAsync(Guid userId, CancellationToken cancellationToken);
    public Task RemoveTokenAsync(RefreshToken token, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}