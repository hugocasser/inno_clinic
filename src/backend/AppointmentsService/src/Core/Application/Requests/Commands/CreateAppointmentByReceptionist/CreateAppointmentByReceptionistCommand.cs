using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CreateAppointmentByReceptionist;

public record CreateAppointmentByReceptionistCommand() : IRequest<OperationResult>;