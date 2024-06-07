using Application.Requests.Resources;
using FluentValidation;

namespace Application.Requests.Commands.Receptionists.CreateReceptionistProfile;

public class CreateReceptionistProfileCommandValidator : AbstractValidator<CreateReceptionistProfileCommand>
{
    public CreateReceptionistProfileCommandValidator()
    {
        RuleFor(command => command.FirstName)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.FirstNameIsRequred)
            .MaximumLength(100)
            .WithMessage(ValidationErrorMessages.FirstNameToLong);
        
        RuleFor(command => command.LastName)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.LastNameIsRequered)
            .MaximumLength(100)
            .WithMessage(ValidationErrorMessages.LastNameToLong);
    }
}