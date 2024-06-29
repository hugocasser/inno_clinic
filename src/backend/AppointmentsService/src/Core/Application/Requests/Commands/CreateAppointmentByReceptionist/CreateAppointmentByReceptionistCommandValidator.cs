using Application.Common;
using Application.Resources;
using FluentValidation;

namespace Application.Requests.Commands.CreateAppointmentByReceptionist;

public class CreateAppointmentByReceptionistCommandValidator 
    : AbstractValidator<CreateAppointmentByReceptionistCommand>
{
    public CreateAppointmentByReceptionistCommandValidator()
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