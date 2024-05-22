using Application.Abstractions.Results;
using MediatR;

namespace Application.Requests.Commands.RegisterDoctor;

public record RegisterDoctorCommand(string Email, string Password) : IRequest<IResult>;