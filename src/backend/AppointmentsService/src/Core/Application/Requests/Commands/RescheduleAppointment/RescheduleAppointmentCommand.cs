using Application.Result;
using MediatR;

namespace Application.Requests.Commands.RescheduleAppointment;

public record RescheduleAppointmentCommand() : IRequest<OperationResult>;