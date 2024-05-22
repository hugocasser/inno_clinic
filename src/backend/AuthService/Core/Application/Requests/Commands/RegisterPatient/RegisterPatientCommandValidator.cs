using Application.Common.Validation;
using FluentValidation;

namespace Application.Requests.Commands.RegisterPatient;

public class RegisterPatientCommandValidator : AbstractValidator<RegisterPatientCommand>
{
    public RegisterPatientCommandValidator()
    {
        RuleFor(command => command.Email).Email();

        RuleFor(command => command.Password).Password();
    }
}