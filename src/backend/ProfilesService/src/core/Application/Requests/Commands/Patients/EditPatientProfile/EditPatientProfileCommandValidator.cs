using Application.Requests.Resources;
using FluentValidation;

namespace Application.Requests.Commands.Patients.EditPatientProfile;

public class EditPatientProfileCommandValidator : AbstractValidator<EditPatientProfileCommand>
{
    public EditPatientProfileCommandValidator()
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
        
        RuleFor(command => command.BirthDate)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.BirthdayIsRequred);
    }
}