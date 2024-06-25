using FluentValidation;

namespace Presentation.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x).NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be between 8 and 32 characters long")
            .MaximumLength(32).WithMessage("Password must be between 8 and 32 characters long")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\?\*\.\(\)\-\$\%\&\@\-]+").WithMessage("Password must contain at least one (!? *. () - $ % & @).");
    }
}