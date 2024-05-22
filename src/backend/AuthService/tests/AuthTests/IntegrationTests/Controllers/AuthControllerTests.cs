using System.Net.Http.Json;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Options;
using Application.Requests.Commands.ConfirmMail;
using Application.Requests.Commands.Login;
using Application.Requests.Commands.RefreshToken;
using Application.Requests.Commands.RegisterPatient;
using Application.Requests.Commands.SingOut;
using Application.Services.Auth;
using AuthTests.IntegrationTests.Fixtures;
using Bogus;
using Domain.Models;
using FluentAssertions;
using Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AuthTests.IntegrationTests.Controllers;

[Collection("Integration Tests")]
public class AuthControllerTests : IClassFixture<ControllersAppFactoryFixture>
{
    private readonly HttpClient _client;
    private readonly ControllersAppFactoryFixture _factory;
    private readonly IOptions<ReceptionistSeedOptions> _receptionistSeedOptions;
    private readonly User _user = TestUtils.FakeUser();
    
    public AuthControllerTests(ControllersAppFactoryFixture factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        
        using var scope = _factory.Services.CreateScope();
        _receptionistSeedOptions = scope.ServiceProvider.GetRequiredService<IOptions<ReceptionistSeedOptions>>();
    }

    [Fact]
    public async Task RegisterPatientAsync_ShouldReturnNoContent_WhenValid()
    {
        // Arrange
        var faker = new Faker();
        var registerPatientCommand = new RegisterPatientCommand(faker.Internet.Email(), faker.Internet.Password()+"-0K");
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register-patient", registerPatientCommand);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task RegisterPatientAsync_ShouldReturnBadRequest_WhenValidationFailed()
    {
        // Arrange 
        var registerPatientCommand = new RegisterPatientCommand(string.Empty, string.Empty);
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register-patient", registerPatientCommand);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task RegisterPatientAsync_ShouldReturnBadRequest_WhenUserAlreadyExist()
    {
        
        // Arrange
        var registerPatientCommand = new RegisterPatientCommand(_receptionistSeedOptions.Value.Email, _receptionistSeedOptions.Value.Password);
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register-patient", registerPatientCommand);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task LoginAsync_ShouldReturnOk_WhenValid()
    {
        // Arrange
        var faker = new Faker();
        var loginUserCommand = new LoginUserCommand(_receptionistSeedOptions.Value.Email, _receptionistSeedOptions.Value.Password);
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginUserCommand);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task LoginAsync_ShouldReturnBadRequest_WhenValidationFailed()
    {
        // Arrange
        var loginUserCommand = new LoginUserCommand(string.Empty, string.Empty);
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginUserCommand);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnUnauthorized_WhenUserNotFound()
    {
        // Arrange
        var faker = new Faker();
        var loginUserCommand = new LoginUserCommand(faker.Internet.Email(), faker.Internet.Password()+"-0K");
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginUserCommand);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnUnauthorized_WhenPasswordIsWrong()
    {
        // Arrange
        var loginUserCommand = new LoginUserCommand(_receptionistSeedOptions.Value.Email, _receptionistSeedOptions.Value.Password+"-0K");
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginUserCommand);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task LogoutAsync_ShouldReturnNoContent_WhenValid()
    {
        // Arrange
        var singOutCommand = new SingOutCommand();
        await Authorized();
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/logout", singOutCommand);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task LogoutAsync_ShouldReturnUnauthorized_WhenNotAuthorized()
    {
        // Arrange
        var singOutCommand = new SingOutCommand();
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/logout", singOutCommand);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task RefreshAsync_ShouldReturnOk_WhenValid()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = _user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpiryTime = DateTime.UtcNow.AddMonths(2),
            AddedTime = DateTime.UtcNow,
            User = _user
        };
        var repository = scope.ServiceProvider.GetRequiredService<IRefreshTokensRepository>();
        await repository.CreateTokenAsync(refreshToken, default);
        await repository.SaveChangesAsync(default);
        
        await Authorized();
        
        // Act
        var response = await _client.PostAsync($"/api/auth/refresh-token/{refreshToken.Token}", null);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task RefreshAsync_ShouldReturnUnauthorized_WhenTokenNotExist()
    {
        // Arrange
        await Authorized();
        
        // Act
        var response = await _client.PostAsync($"/api/auth/refresh-token/{Guid.NewGuid()}", null);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task RefreshAsync_ShouldReturnUnauthorized_WhenJwtTokenIsNotPassed()
    {
        // Act
        var response = await _client.PostAsync($"/api/auth/refresh-token/{Guid.NewGuid()}", null);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ConfirmEmailAsync_ShouldReturnOk_WhenValid()
    {
        // Arrange
        var scope = _factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        await SeedUser();
        var code = await userManager.GenerateEmailConfirmationTokenAsync(_user);

        var command = new ConfirmMailCommand(_user.Id, code);
        // Act
        var response = await _client.PostAsJsonAsync($"/api/auth/confirm-mail", command);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task ConfirmEmailAsync_ShouldReturnNotFound_WhenUserNotExists()
    {
        // Arrange
        var scope = _factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        _user.SecurityStamp = "test";
        var code = await userManager.GenerateEmailConfirmationTokenAsync(_user);

        var command = new ConfirmMailCommand(_user.Id, code);
        // Act
        var response = await _client.PostAsJsonAsync($"/api/auth/confirm-mail", command);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ConfirmEmailAsync_ShouldReturnBadRequest_WhenCodeIsInvalid()
    {
        // Arrange
        var scope = _factory.Services.CreateScope();
        await SeedUser();

        var command = new ConfirmMailCommand(_user.Id, "not-valid-code");
        // Act
        var response = await _client.PostAsJsonAsync($"/api/auth/confirm-mail", command);
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    private void Unauthorized()
    {
        _client.DefaultRequestHeaders.Remove("Authorization");
    }
    
    private async Task Authorized()
    {
        using var scope = _factory.Services.CreateScope();
        var jwtOptions = scope.ServiceProvider.GetRequiredService<IOptions<AccessTokenOptions>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var tokenService = new AccessTokensService(jwtOptions, userManager);
        await userManager.CreateAsync(_user);
        var token = tokenService.CreateAccessToken(_user,  new List<string> { nameof(Roles.Receptionist) } );
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    }

    private async Task SeedUser()
    {
        using var scope = _factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        _user.PasswordHash = default;
        var password = "1234567890-Ok";
        _user.UserName = "test";
        await userManager.CreateAsync(_user);
    }
}