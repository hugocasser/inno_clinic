using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CreateAppointmentResult;

public record CreateAppointmentResultCommand() : IRequest<OperationResult>;