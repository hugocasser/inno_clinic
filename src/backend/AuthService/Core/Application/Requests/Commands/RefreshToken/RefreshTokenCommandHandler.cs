using Application.Abstractions.Auth;
using Application.Abstractions.Results;
using Application.Abstractions.Services;
using Application.Dtos.Views;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using MediatR;

namespace Application.Requests.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IRefreshTokensService refreshTokensService,
    IAccessTokensService accessTokensService, 
    IHttpContextAccessorExtension httpContext)
    : IRequestHandler<RefreshTokenCommand, IResult>
{
    public async Task<IResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        
        var result = await refreshTokensService.ValidateRefreshTokenAsync(httpContext.GetUserId(), request.RefreshToken, cancellationToken);

        if (result is not ResultWithContent<Domain.Models.RefreshToken> success)
        {
            return ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidRefreshToken));
        }

        var roles = await accessTokensService.GetRolesAsync(success.ResultData.User);

        var token = accessTokensService.CreateAccessToken(success.ResultData.User, roles, cancellationToken);

        if (string.IsNullOrEmpty(token))
        {
            return ResultWithoutContent.Failure(Error.Unauthorized()
                .WithMessage(ErrorMessages.UserNotHaveSuitableRole));
        }

        return ResultWithContent<AuthTokenViewDto>.Success(
            AuthTokenViewDto.FromModel(success.ResultData.User, token, success.ResultData));
    }
}