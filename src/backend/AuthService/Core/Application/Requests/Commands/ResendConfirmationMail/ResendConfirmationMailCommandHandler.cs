using Application.Abstractions.Auth;
using Application.Abstractions.Results;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Requests.Commands.ResendConfirmationMail;

public class ResendConfirmationMailCommandHandler(UserManager<User> usersManager,
    IConfirmMessageSenderService confirmMessageSender,
    IUserService userService) : IRequestHandler<ResendConfirmationMailCommand, IResult>
{
    public async Task<IResult> Handle(ResendConfirmationMailCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.CheckUserPassword(request.Email, request.Password);
        
        if (result is not ResultWithContent<User> success)
        {
            return result;
        }
        
        if (success.ResultData.EmailConfirmed)
        {
            return ResultWithoutContent.Failure(Error.BadRequest().WithMessage(ErrorMessages.EmailAlreadyConfirmed));
        }
        
        await confirmMessageSender.SendEmailConfirmMessageAsync(success.ResultData, cancellationToken);
        
        return ResultWithoutContent.Success();
    }
}