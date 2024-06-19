using Application.Abstractions;
using Application.Results;
using Presentation.Abstractions.Services;
using Presentation.Common;

namespace Presentation.Services;

public class CredentialsService : ICredentialsService
{
    public Task<IResult> CheckLoginAsync()
    {
        return Task.FromResult<IResult>(new Result());
    }

    public Task<IResult> TryLoginAsync(string loginModelEmail, string loginModelPassword)
    {
        return Task.FromResult<IResult>(new Result());
    }

    public Task<IResult> LogoutAsync()
    {
        SecureStorage.Default.Remove(nameof(SecureStorageItemsNames.Id));
        SecureStorage.Default.Remove(nameof(SecureStorageItemsNames.AccessToken));
        SecureStorage.Default.Remove(nameof(SecureStorageItemsNames.RefreshToken));
        return Task.FromResult<IResult>(new Result());
    }

    public async Task<IResult> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var id = await SecureStorage.Default.GetAsync(nameof(SecureStorageItemsNames.Id));

        if (string.IsNullOrEmpty(id))
        {
            await LogoutAsync();

            return new Result();
        }
        
        var accessToken = await SecureStorage.Default.GetAsync(nameof(SecureStorageItemsNames.AccessToken));

        if (string.IsNullOrEmpty(accessToken))
        {
            await LogoutAsync();

            return new Result();
        }

        return new Result();
    }

    public Task<IResult> TryRefreshTokenAsync(string accessToken)
    {
        return Task.FromResult<IResult>(new Result());
    }

    public string GetRoleFromToken()
    {
        return nameof(EnumRoles.Receptionist);
    }
}