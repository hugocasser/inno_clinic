using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Results;
using Presentation.Abstractions.Services;
using Presentation.Common;

namespace Presentation.Services;

public class CredentialsService(IAuthService authService, IProfilesService profilesService) : ICredentialsService
{
    public Task<IResult> CheckLoginAsync()
    {
         return authService.CheckLoginAsync();
    }

    public async Task<IResult> TryLoginAsync(string loginModelEmail, string loginModelPassword)
    {
        var result = await authService.LoginAsync(loginModelEmail, loginModelPassword);
        var resultData = result.GetResultData<AuthResponseDto>();
        
        if (!result.IsSuccess || resultData is null)
        {
            return result;
        }
        
        await SecureStorage.Default.SetAsync(nameof(SecureStorageItemsNames.Id), resultData.Id.ToString());
        await SecureStorage.Default.SetAsync(nameof(SecureStorageItemsNames.AccessToken), resultData.AccessToken);
        await SecureStorage.Default.SetAsync(nameof(SecureStorageItemsNames.RefreshToken), resultData.RefreshToken);
        await SecureStorage.Default.SetAsync(nameof(SecureStorageItemsNames.Role), resultData.Role);
        
        return new Result();
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

    public async Task<IResult> TryRefreshTokenAsync()
    {
        var refreshToken = await SecureStorage.Default.GetAsync(nameof(SecureStorageItemsNames.RefreshToken));
        
        if (string.IsNullOrEmpty(refreshToken))
        {
            return new Result()
            {
                IsSuccess = false
            };
        }
        
        var accessToken = await SecureStorage.Default.GetAsync(nameof(SecureStorageItemsNames.AccessToken));
        
        if (string.IsNullOrEmpty(accessToken))
        {
            return new Result()
            {
                IsSuccess = false
            };
        }
        
        var result = await authService.TryRefreshTokenAsync(accessToken,refreshToken);
        
        var resultData = result.GetResultData<AuthResponseDto>();

        if (resultData is null || !result.IsSuccess)
        {
            return new Result()
            {
                IsSuccess = false
            };
        }
        
        await SecureStorage.Default.SetAsync(nameof(SecureStorageItemsNames.Id), resultData.Id.ToString());
        await SecureStorage.Default.SetAsync(nameof(SecureStorageItemsNames.AccessToken), resultData.AccessToken);
        await SecureStorage.Default.SetAsync(nameof(SecureStorageItemsNames.RefreshToken), resultData.RefreshToken);
        await SecureStorage.Default.SetAsync(nameof(SecureStorageItemsNames.Role), resultData.Role);
        
        return new Result();
    }

    public string GetRoleFromToken()
    {
        return nameof(EnumRoles.Receptionist);
    }
}