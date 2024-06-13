using Application.Abstractions;

namespace Presentation.Abstractions.Services;

public interface ICredentialsService
{
    public Task<bool> CheckLoginAsync();
    public Task<IResult> TryLoginAsync(string loginModelEmail, string loginModelPassword);
}