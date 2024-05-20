using Application.Abstractions.Results;
using MediatR;

namespace Application.Requests.Commands.ResendConfirmationMail;

public record ResendConfirmationMailCommand(string Email, string Password) : IRequest<IResult>;