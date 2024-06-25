using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Results;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    public Task<IResult> LoginAsync(string email, string password)
    {
        var result = new Result();
        
        var authResponseDto = new AuthResponseDto(Guid.NewGuid(), "token", "refreshToken", "role");
        
        result.SetResultData(authResponseDto);
        
        return Task.FromResult<IResult>(result);
    }
    
    public Task<IResult> CheckLoginAsync()
    {
        return Task.FromResult<IResult>(new Result());
    }

    public Task<IResult> TryRefreshTokenAsync(string accessToken, string refreshToken)
    {
        var result = new Result();
        
        var authResponseDto = new AuthResponseDto(Guid.NewGuid(), "token", "refreshToken", "role");
        
        result.SetResultData(authResponseDto);
        
        return Task.FromResult<IResult>(result);
    }
}