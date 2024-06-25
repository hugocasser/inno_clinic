using Application.Result;
using MediatR;

namespace Application.Requests.Commands.SelectDateTime;

public record SelectDateTimeCommand() : IRequest<OperationResult>;