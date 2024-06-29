using Application.Result;
using MediatR;

namespace Application.Requests.Commands.RescheduleAppointment;

public record RescheduleAppointmentCommand(Guid AppointmentId, DateTimeOffset NewDate) : IRequest<OperationResult>;