namespace Application.Abstractions.Services;

public interface IAuthService
{
    public Task<IResult> LoginAsync(string email, string password);
    public Task<IResult> CheckLoginAsync();
    public Task<IResult> TryRefreshTokenAsync(string accessToken, string refreshToken);
}