using Application.Resources;
using FluentValidation;

namespace Application.Requests.Commands.CreateAppointment;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.Start)
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeEmpty);
        
        RuleFor(x => x.End)
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeEmpty);
        
        RuleFor(x => x.Start)
            .LessThan(x => x.End)
            .WithMessage(ValidationMessages.EndMustBeGreateThenStart);
    }
}