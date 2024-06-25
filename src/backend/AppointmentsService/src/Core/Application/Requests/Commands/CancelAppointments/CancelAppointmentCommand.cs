using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CancelAppointments;

public record CancelAppointmentCommand(Guid Id) : IRequest<OperationResult>;