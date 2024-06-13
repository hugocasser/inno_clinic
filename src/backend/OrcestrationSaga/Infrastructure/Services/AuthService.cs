using Application.Abstractions;
using Application.Abstractions.Services.External;
using Application.Dtos.Common;
using Application.Result;

namespace Infrastructure.Services;

public class AuthService : IAuthService 
{
    
    //TODO: add logic to create account
    // now it's just mock while services communication not implemented
    public Task<IResult> CreateAccountAsync(string requestEmail, string requestPassword, EnumRoles role,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }

    public Task<IResult> TryRollbackAsync(Guid accountId, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }

    public Task<IResult> DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }
}