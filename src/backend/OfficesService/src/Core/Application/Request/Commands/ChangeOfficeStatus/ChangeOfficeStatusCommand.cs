using Application.Abstractions.OperationResult;
using MediatR;

namespace Application.Request.Commands.ChangeOfficeStatus;

public record ChangeOfficeStatusCommand(Guid OfficeId, bool IsActive) : IRequest<IResult>;