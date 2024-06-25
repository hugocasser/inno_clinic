namespace Application.Dtos;

public record AuthResponseDto(Guid Id, string AccessToken, string RefreshToken, string Role);