using FluentValidation;
using Presentation.Resources;

namespace Presentation.Validators;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x).NotEmpty().WithMessage(ValidationMessages.EmailNotValid);
        RuleFor(x => x).EmailAddress().WithMessage(ValidationMessages.EmailNotValid);
    }
}