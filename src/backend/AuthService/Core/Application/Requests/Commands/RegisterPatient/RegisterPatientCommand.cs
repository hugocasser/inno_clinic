using Application.Abstractions.Results;
using MediatR;

namespace Application.Requests.Commands.RegisterPatient;

public record RegisterPatientCommand(string Email, string Password) : IRequest<IResult>;