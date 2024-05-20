using Application.Common.Validation;
using FluentValidation;

namespace Application.Requests.Commands.RegisterDoctor;

public class RegisterDoctorCommandValidator : AbstractValidator<RegisterDoctorCommand>
{
    public RegisterDoctorCommandValidator()
    {
        RuleFor(command => command.Email).Email();
        
        RuleFor(command => command.Password).Password();
    }
}