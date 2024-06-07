using System.Diagnostics.CodeAnalysis;
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
[ExcludeFromCodeCoverage]
public class AuthController(ISender sender) : ApiController(sender)
{
    
    [HttpPost]
    [Route("register-patient")]
    public async Task<IActionResult> RegisterPatientAsync([FromBody] RegisterPatientCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
    
    [HttpPost]
    [Authorize(Roles = nameof(Roles.Receptionist))]
    [Route("register-doctor")]
    public async Task<IActionResult> RegisterDoctorAsync([FromBody] RegisterDoctorCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
    
    [HttpPost]
    [Route("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutAsync([FromBody] SingOutCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
    
    [HttpPost]
    [Route("refresh-token/{token}")]
    public async Task<IActionResult> RefreshAsync([FromRoute] string token,CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new RefreshTokenCommand(token), cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
    
    [HttpPost]
    [Route("confirm-mail")]
    public async Task<IActionResult> ConfirmMailAsync([FromBody]ConfirmMailCommand command,  CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        
        return ResultProcessing.FromResult(result);
    }
}