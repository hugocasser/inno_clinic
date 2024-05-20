using Application.Abstractions.Results;
using MediatR;

namespace Application.Requests.Commands.ConfirmMail;

public record ConfirmMailCommand(Guid UserId, string Code) : IRequest<IResult>;