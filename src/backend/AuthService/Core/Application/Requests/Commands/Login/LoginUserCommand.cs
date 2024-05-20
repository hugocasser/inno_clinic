using Application.Abstractions.Results;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Commands.Login;

public record LoginUserCommand(string Email, string Password) : IRequest<IResult>;