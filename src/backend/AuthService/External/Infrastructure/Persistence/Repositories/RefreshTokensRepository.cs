using Application.Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RefreshTokensRepository(AuthDbContext context) : IRefreshTokensRepository
{
    public async Task CreateTokenAsync(RefreshToken token, CancellationToken cancellationToken)
    {
        await context.RefreshTokens.AddAsync(token, cancellationToken);
    }

    public async Task<RefreshToken?> GetUserRefreshTokenAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await context.RefreshTokens
            .Include(token => token.User)
            .FirstOrDefaultAsync(x =>
                x.UserId == userId, cancellationToken);
    }

    public Task RemoveTokenAsync(RefreshToken token)
    {
        context.RefreshTokens.Remove(token);
        
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}