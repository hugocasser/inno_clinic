using Application.Abstractions.Auth;
using Application.Abstractions.Results;
using Application.Abstractions.Services;
using Application.Dtos.Views;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Requests.Commands.Login;

public class LoginUserCommandHandler(
    UserManager<User> userManager,
    IRefreshTokensService refreshTokensService,
    IAccessTokensService accessTokensService) : IRequestHandler<LoginUserCommand, IResult>
{
    public async Task<IResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var checkUserPasswordResult = await CheckUserPassword(request);

        if (checkUserPasswordResult is not ResultWithContent<User> success)
        {
            return checkUserPasswordResult;
        }

        var refreshToken = await refreshTokensService.CreateUserRefreshTokenAsync(success.ResultData, cancellationToken);
        var userRoles = await accessTokensService.GetRolesAsync(success.ResultData);
        var accessToken = accessTokensService.CreateAccessToken(success.ResultData,userRoles, cancellationToken);

        if (string.IsNullOrEmpty(accessToken))
        {
            return ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.UserNotHaveSuitableRole));
        }
        
        return ResultWithContent<AuthTokenViewDto>
            .Success(AuthTokenViewDto.FromModel(success.ResultData, accessToken, refreshToken));
    }

    private async Task<IResult> CheckUserPassword(LoginUserCommand request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword));
        }

        var passwordCheckResult = await userManager.CheckPasswordAsync(user, request.Password);

        if (!passwordCheckResult)
        {
            return ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword));
        }

        return ResultWithContent<User>.Success(user);
    }
}