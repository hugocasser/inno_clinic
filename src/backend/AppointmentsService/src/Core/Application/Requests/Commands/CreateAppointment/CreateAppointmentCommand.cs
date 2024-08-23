using Application.Result;
using Domain.Models;
using MediatR;

namespace Application.Requests.Commands.CreateAppointment;

public record CreateAppointmentCommand(
    Guid PatientId,
    Guid DoctorId,
    Guid OfficeId,
    Guid ServiceId,
    DateTimeOffset Start) : IRequest<OperationResult>
{
    public Appointment MapToAppointment()
    {
        return new Appointment
        {
            PatientId = PatientId,
            DoctorId = DoctorId,
            OfficeId = OfficeId,
            ServiceId = ServiceId,
            IsApproved = false,
            Date = DateOnly.FromDateTime(Start.Date),
            Time = TimeOnly.FromDateTime(Start.DateTime)
        };
    }
};