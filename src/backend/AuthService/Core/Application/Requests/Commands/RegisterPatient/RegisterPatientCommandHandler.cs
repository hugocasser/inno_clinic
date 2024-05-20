using Application.Abstractions.Auth;
using Application.Abstractions.Results;
using Domain.Models;
using MediatR;

namespace Application.Requests.Commands.RegisterPatient;

public class RegisterPatientCommandHandler(IUserService userService)
    : IRequestHandler<RegisterPatientCommand, IResult>
{
    public Task<IResult> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
    {
        return userService.RegisterUser(request.Email, request.Password, Roles.Patient, cancellationToken);
    }
}