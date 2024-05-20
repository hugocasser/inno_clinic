using Application.Requests.Commands.ConfirmMail;
using Application.Requests.Commands.Login;
using Application.Requests.Commands.RefreshToken;
using Application.Requests.Commands.RegisterDoctor;
using Application.Requests.Commands.RegisterPatient;
using Application.Requests.Commands.SingOut;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Common;

namespace Presentation.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/auth")]
public class AuthController(ISender sender) : ApiController(sender)
{
    [HttpPost("/register-patient")]
    public async Task<IActionResult> RegisterPatientAsync([FromBody] RegisterPatientCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
    
    [HttpPost("/register-doctor")]
    [Authorize(Roles = nameof(Roles.Receptionist))]
    public async Task<IActionResult> RegisterDoctorAsync([FromBody] RegisterDoctorCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
    
    [HttpPost("/login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
    
    [HttpPost("/logout")]
    [Authorize]
    public async Task<IActionResult> LogoutAsync([FromBody] SingOutCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
    
    [HttpPost("/refresh-token/{token}")]
    [Authorize]
    public async Task<IActionResult> RefreshAsync([FromRoute] string token,CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RefreshTokenCommand(token), cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
    
    [HttpPost("/confirm-mail/{userId}/{code}")]
    public async Task<IActionResult> ConfirmMailAsync([FromRoute]Guid userId, [FromRoute] string code, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new ConfirmMailCommand(userId,code), cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
}