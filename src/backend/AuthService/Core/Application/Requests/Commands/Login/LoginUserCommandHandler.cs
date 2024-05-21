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
    IAccessTokensService accessTokensService,
    IUserService userService) : IRequestHandler<LoginUserCommand, IResult>
{
    public async Task<IResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var checkUserPasswordResult = await userService.CheckUserPassword(request.Email, request.Password);

        if (checkUserPasswordResult is not ResultWithContent<User> success)
        {
            return checkUserPasswordResult;
        }
        
        var userRoles = await accessTokensService.GetRolesAsync(success.ResultData);
        var accessToken = accessTokensService.CreateAccessToken(success.ResultData,userRoles, cancellationToken);

        if (string.IsNullOrEmpty(accessToken))
        {
            return ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.UserNotHaveSuitableRole));
        }
        
        var refreshToken = await refreshTokensService.CreateUserRefreshTokenAsync(success.ResultData, cancellationToken);
        
        return ResultWithContent<AuthTokenViewDto>
            .Success(AuthTokenViewDto.FromModel(success.ResultData, accessToken, refreshToken));
    }
}