using Application.Abstractions.Auth;
using Application.Abstractions.Results;
using Application.Common;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Auth;

public class UsersService(UserManager<User> userManager, IConfirmMessageSenderService confirmMessageSender) : IUserService
{
    public async Task<IResult> RegisterUser(string email, string password, Roles role, CancellationToken cancellationToken)
    {
        if (userManager.FindByEmailAsync(email).Result != null)
        {
            return ResultWithoutContent.Failure(Error.BadRequest().WithMessage(ErrorMessages.UserAlreadyExists));
        }
        
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = email
        };
        
        var creationResult = await userManager.CreateAsync(user, password);
        var result = Utilities.AggregateIdentityResult(creationResult);

        if (!result.IsSuccess)
        {
            return result;
        }
        
        var addToRoleResult = Utilities.AggregateIdentityResult(await userManager.AddToRoleAsync(user, role.ToString()));

        if (!addToRoleResult.IsSuccess)
        {
            return addToRoleResult;
        }

        await confirmMessageSender.SendEmailConfirmMessageAsync(user, cancellationToken);
        
        return ResultWithoutContent.Success();
    }
}