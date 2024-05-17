using Domain.Models;

namespace Application.Dtos.Views;

public record AuthTokenViewDto(Guid UserId, string AccessToken, string RefreshToken)
{
    public static AuthTokenViewDto FromModel(User user, string accessToken, RefreshToken refreshToken)
    {
        return new AuthTokenViewDto(user.Id, accessToken, refreshToken.Token);
    }
};