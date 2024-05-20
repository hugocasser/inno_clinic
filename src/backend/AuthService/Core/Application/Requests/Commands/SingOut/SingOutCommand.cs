using Application.Abstractions.Results;
using MediatR;

namespace Application.Requests.Commands.SingOut;

public record SingOutCommand() : IRequest<IResult>;