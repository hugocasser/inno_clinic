using Application.Abstractions.Auth;
using Application.Abstractions.Results;
using Domain.Models;
using MediatR;

namespace Application.Requests.Commands.RegisterDoctor;

public class RegisterDoctorCommandHandler(IUserService userService) : IRequestHandler<RegisterDoctorCommand, IResult>
{
    public Task<IResult> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
    {
        return userService.RegisterUser(request.Email, request.Password, Roles.Doctor, cancellationToken);
    }
}