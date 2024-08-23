using Application.Result;
using MediatR;

namespace Application.Requests.Commands.ApproveAppointment;

public record ApproveAppointmentCommand(Guid Id) : IRequest<OperationResult>;