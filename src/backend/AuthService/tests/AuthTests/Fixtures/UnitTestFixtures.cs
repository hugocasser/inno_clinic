using Application.Abstractions.Auth;
using Application.Abstractions.Cache;
using Application.Abstractions.Email;
using Application.Abstractions.Repositories;
using Application.Abstractions.Results;
using Application.Abstractions.Services;
using Application.Options;
using Application.PipelineBehaviors;
using Application.Requests.Commands.ConfirmMail;
using Application.Requests.Commands.Login;
using Application.Requests.Commands.RefreshToken;
using Application.Requests.Commands.RegisterDoctor;
using Application.Requests.Commands.RegisterPatient;
using Application.Requests.Commands.ResendConfirmationMail;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuthTests.Fixtures;

public class UnitTestFixtures
{
    protected  ConfirmMailCommandValidator ConfirmMailCommandValidator => new();
    protected  RefreshTokenCommandValidator RefreshTokenCommandValidator => new();
    protected  LoginUserCommandValidator LoginUserCommandValidator => new();
    protected RegisterDoctorCommandValidator RegisterDoctorCommandValidator => new();
    protected  RegisterPatientCommandValidator RegisterPatientCommandValidator => new();
    protected  ResendConfirmationMailCommandValidator ResendConfirmationMailCommandValidator => new();
    
    
    protected  RegisterDoctorCommand CreateRegisterDoctorCommand(string email, string password) => new(email, password);
    protected RegisterPatientCommand CreateRegisterPatientCommand(string email, string password) => new(email, password);
    protected      ConfirmMailCommand CreateConfirmMailCommand(Guid userId, string code) => new(userId, code);
    protected  RefreshTokenCommand CreateRefreshTokenCommand(string refreshToken) => new(refreshToken);
    protected  LoginUserCommand CreateLoginUserCommand(string email, string password) => new(email, password);
    protected  ResendConfirmationMailCommand CreateResendConfirmationMailCommand(string email, string password) => new(email, password);
    
    protected readonly Mock<UserManager<User>> UserManagerMock = new(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    protected readonly Mock<IConfirmMessageSenderService> _confirmMessageSenderMock = new();
    protected readonly Mock<IUserService> UserServiceMock = new();
    protected readonly Mock<IRefreshTokensService> RefreshTokensServiceMock = new();
    protected readonly Mock<IAccessTokensService> AccessTokensServiceMock = new();
    protected readonly Mock<ILogger<LoggingPipelineBehavior<LoginUserCommand, IResult>>> Logger = new();

    protected readonly IEnumerable<IValidator<LoginUserCommand>> Validators = new List<IValidator<LoginUserCommand>>() 
    { 
        new LoginUserCommandValidator()
    };

    protected readonly Mock<IOptions<AccessTokenOptions>> MockAccessOptions = new();
    protected IAccessTokensService AccessTokensService;
    protected readonly Mock<IEmailSenderService> MockEmailSenderService = new();
    protected readonly Mock<IOptions<EmailSenderOptions>> MockEmailOptions = new();
    protected IConfirmMessageSenderService ConfirmMessageSenderService;
    protected readonly Mock<ICacheService> CacheService = new();
    protected readonly Mock<IRefreshTokensRepository> RefreshTokensRepository = new();
    protected IRefreshTokensService RefreshTokenService;
    protected IUserService UsersService;
}