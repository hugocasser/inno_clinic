using Application.Dtos.Common;

namespace Application.Abstractions.Services.External;

public interface IAuthService
{
    public Task<IResult> CreateAccountAsync(string requestEmail, string requestPassword, EnumRoles role, CancellationToken cancellationToken);
    public Task<IResult> TryRollbackAsync(Guid accountId, CancellationToken cancellationToken);
    public Task<IResult> DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken);
}