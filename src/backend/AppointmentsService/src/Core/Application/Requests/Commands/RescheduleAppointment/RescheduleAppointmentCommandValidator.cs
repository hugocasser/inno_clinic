using Application.Common;
using FluentValidation;

namespace Application.Requests.Commands.RescheduleAppointment;

public class RescheduleAppointmentCommandValidator : AbstractValidator<RescheduleAppointmentCommand>
{
    public RescheduleAppointmentCommandValidator()
    {
        RuleFor(x => x.NewDate)
            .NotEmptyWithMessage();
    }
}