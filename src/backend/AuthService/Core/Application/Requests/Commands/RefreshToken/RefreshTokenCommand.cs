using Application.Abstractions.Results;
using MediatR;

namespace Application.Requests.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<IResult>;