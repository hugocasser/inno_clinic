using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CreateAppointment;

public record CreateAppointmentCommand() : IRequest<OperationResult>;