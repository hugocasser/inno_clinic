using Application.Common;
using Application.Resources;
using FluentValidation;

namespace Application.Requests.Commands.CreateAppointment;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(command => command.Start)
            .NotEmptyWithMessage();
        
        RuleFor(command => command.ServiceId)
            .NotEmptyWithMessage();
        
        RuleFor(command => command.DoctorId)
            .NotEmptyWithMessage();
        
        RuleFor(command => command.OfficeId)
            .NotEmptyWithMessage();
        
        RuleFor(command => command.PatientId)
            .NotEmptyWithMessage();
    }
}