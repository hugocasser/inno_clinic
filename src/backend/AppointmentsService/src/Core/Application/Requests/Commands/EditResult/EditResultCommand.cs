using Application.Result;
using MediatR;

namespace Application.Requests.Commands.EditResult;

public record EditResultCommand() : IRequest<OperationResult>;