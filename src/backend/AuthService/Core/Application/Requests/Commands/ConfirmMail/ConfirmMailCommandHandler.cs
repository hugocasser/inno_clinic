using Application.Abstractions.Results;
using Application.Common;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Requests.Commands.ConfirmMail;

public class ConfirmMailCommandHandler(UserManager<User> usersManager)
    : IRequestHandler<ConfirmMailCommand, IResult>
{
    public async Task<IResult> Handle(ConfirmMailCommand request, CancellationToken cancellationToken)
    {
        var user = await usersManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            return ResultWithoutContent.Failure(Error.NotFound().WithMessage(ErrorMessages.UserNotFound));
        }

        var result = await usersManager.ConfirmEmailAsync(user, request.Code);

        if (!Utilities.AggregateIdentityResult(result).IsSuccess)
        {
            return ResultWithoutContent.Failure(Error.BadRequest().WithMessage(ErrorMessages.InvalidConfirmToken));
        }
        

        return ResultWithoutContent.Success();
    }
}