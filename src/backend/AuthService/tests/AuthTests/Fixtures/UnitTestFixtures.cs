using Application.Abstractions.Auth;
using Application.Abstractions.Cache;
using Application.Abstractions.Email;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AuthTests.Fixtures;

public class UnitTestFixtures
{
    
    protected readonly Mock<UserManager<User>> UserManagerMock = new(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    protected readonly Mock<IConfirmMessageSenderService> _confirmMessageSenderMock = new();
    protected readonly Mock<IUserService> UserServiceMock = new();
    protected readonly Mock<IRefreshTokensService> RefreshTokensServiceMock = new();
    protected readonly Mock<IAccessTokensService> AccessTokensServiceMock = new();
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