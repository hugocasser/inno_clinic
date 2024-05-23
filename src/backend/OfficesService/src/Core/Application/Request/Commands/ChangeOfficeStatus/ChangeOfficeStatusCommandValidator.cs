using FluentValidation;

namespace Application.Request.Commands.ChangeOfficeStatus;

public class ChangeOfficeStatusCommandValidator : AbstractValidator<ChangeOfficeStatusCommand>
{
    public ChangeOfficeStatusCommandValidator()
    {
        RuleFor(x => x.OfficeId).NotEmpty();
        
        RuleFor(x => x.IsActive).NotNull();
    }
}