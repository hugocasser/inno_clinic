using Application.Result;
using Domain.Models;
using MediatR;

namespace Application.Requests.Commands.CreateAppointmentByReceptionist;

public record CreateAppointmentByReceptionistCommand(
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
            IsApproved = true,
            Date = DateOnly.FromDateTime(Start.Date),
            Time = TimeOnly.FromDateTime(Start.DateTime)
        };
    }
};