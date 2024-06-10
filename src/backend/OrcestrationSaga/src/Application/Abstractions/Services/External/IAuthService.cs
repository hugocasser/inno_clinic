using Application.Dtos.Common;

namespace Application.Abstractions.Services.External;

public interface IAuthService
{
    Task<IResult> CreateAccountAsync(string requestEmail, string requestPassword, EnumRoles role, CancellationToken cancellationToken);
    Task<IResult> TryRollbackAsync(Guid getContent, CancellationToken cancellationToken);
}