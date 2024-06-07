using Application.Requests.Resources;
using FluentValidation;

namespace Application.Requests.Commands.Receptionists.EditDoctorsProfile;

public class EditDoctorsProfileCommandValidator : AbstractValidator<EditDoctorsProfileCommand>
{
    public EditDoctorsProfileCommandValidator()
    {
        RuleFor(command => command.FirstName)
            .MaximumLength(100)
            .WithMessage(ValidationErrorMessages.FirstNameToLong);
        
        RuleFor(command => command.LastName)
            .MaximumLength(100)
            .WithMessage(ValidationErrorMessages.LastNameToLong);
    }
}