using FluentValidation;

namespace Application.Requests.Commands.ConfirmMail;

public class ConfirmMailCommandValidator : AbstractValidator<ConfirmMailCommand>
{
    public ConfirmMailCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
    }
}