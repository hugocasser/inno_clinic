using Application.Common.Validation;
using FluentValidation;

namespace Application.Requests.Commands.ResendConfirmationMail;

public class ResendConfirmationMailCommandValidator :  AbstractValidator<ResendConfirmationMailCommand>
{
    public ResendConfirmationMailCommandValidator()
    {
        RuleFor(x => x.Email).Password();
        
        RuleFor(x => x.Password).Email();
    }
}