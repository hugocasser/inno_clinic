using Application.Abstractions.Auth;
using Application.Abstractions.Results;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Requests.Commands.ResendConfirmationMail;

public class ResendConfirmationMailCommandHandler(UserManager<User> usersManager, IConfirmMessageSenderService confirmMessageSender) : IRequestHandler<ResendConfirmationMailCommand, IResult>
{
    public async Task<IResult> Handle(ResendConfirmationMailCommand request, CancellationToken cancellationToken)
    {
        var user = await usersManager.FindByEmailAsync(request.Email);
        if (user is null || !await usersManager.CheckPasswordAsync(user, request.Password))
        {
            return ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword));
        }
        
        if (user.EmailConfirmed)
        {
            return ResultWithoutContent.Failure(Error.BadRequest().WithMessage(ErrorMessages.EmailAlreadyConfirmed));
        }
        
        await confirmMessageSender.SendEmailConfirmMessageAsync(user, cancellationToken);
        
        return ResultWithoutContent.Success();
    }
}