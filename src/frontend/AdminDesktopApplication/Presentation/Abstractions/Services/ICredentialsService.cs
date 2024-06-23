using Application.Abstractions;

namespace Presentation.Abstractions.Services;

public interface ICredentialsService
{
    public Task<IResult> CheckLoginAsync();
    public Task<IResult> TryLoginAsync(string loginModelEmail, string loginModelPassword);
    public Task<IResult> LogoutAsync();
    public Task<IResult> GetCurrentUserAsync(CancellationToken cancellationToken);
    public Task<IResult> TryRefreshTokenAsync();
    public string GetRoleFromToken();
}