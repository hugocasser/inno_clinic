using Application.Abstractions.Services;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using IResult = Application.Abstractions.Results.IResult;

namespace Application.Requests.Commands.SingOut;

public class SingOutCommandHandler
    (UserManager<User> userManager,
        IRefreshTokensService refreshTokensService,
        IHttpContextAccessorExtension contextAccessor) 
    : IRequestHandler<SingOutCommand, IResult>
{
    public async Task<IResult> Handle(SingOutCommand request, CancellationToken cancellationToken)
    {
        var userId = contextAccessor.GetUserId();

        var result = await refreshTokensService.RevokeRefreshTokenAsync(userId, cancellationToken);
        
        return result;
    }
}