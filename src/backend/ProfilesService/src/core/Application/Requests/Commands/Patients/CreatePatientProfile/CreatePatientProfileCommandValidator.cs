using Application.Requests.Resources;
using FluentValidation;

namespace Application.Requests.Commands.Patients.CreatePatientProfile;

public class CreatePatientProfileCommandValidator : AbstractValidator<CreatePatientProfileCommand>
{
    public CreatePatientProfileCommandValidator()
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
        
        RuleFor(command => command.UserId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.UserIdIsRequered);
    }
}