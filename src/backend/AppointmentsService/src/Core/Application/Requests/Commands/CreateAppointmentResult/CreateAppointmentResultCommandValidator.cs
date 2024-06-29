using Application.Common;
using FluentValidation;

namespace Application.Requests.Commands.CreateAppointmentResult;

public class CreateAppointmentResultCommandValidator : AbstractValidator<CreateAppointmentResultCommand>
{
    public CreateAppointmentResultCommandValidator()
    {
        RuleFor(command => command.Complaints)
            .ResultDescriptionString();
        
        RuleFor(command => command.Conclusion)
            .ResultDescriptionString();
        
        RuleFor(command => command.Recommendation)
            .ResultDescriptionString();
        
        RuleFor(command => command.AppointmentId)
            .NotEmptyWithMessage();
    }
}