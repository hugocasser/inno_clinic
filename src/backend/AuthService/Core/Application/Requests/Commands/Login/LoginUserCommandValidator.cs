using Application.Common.Validation;
using FluentValidation;

namespace Application.Requests.Commands.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.Email).Email();

        RuleFor(command => command.Password).Password();
    }
}