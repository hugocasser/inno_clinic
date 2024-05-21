using Application.Common.Validation;
using FluentValidation;

namespace Application.Requests.Commands.ResendConfirmationMail;

public class ResendConfirmationMailCommandValidator :  AbstractValidator<ResendConfirmationMailCommand>
{
    public ResendConfirmationMailCommandValidator()
    {
        RuleFor(x => x.Email).Email();
        
        RuleFor(x => x.Password).Password();
    }
}