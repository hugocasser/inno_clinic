using Application.Abstractions;
using Application.Results;
using Presentation.Abstractions.Services;

namespace Presentation.Services;

public class CredentialsService : ICredentialsService
{
    public Task<bool> CheckLoginAsync()
    {
        return Task.FromResult(false);
    }

    public Task<IResult> TryLoginAsync(string loginModelEmail, string loginModelPassword)
    {
        return Task.FromResult<IResult>(new Result());
    }
}