using Application.Abstractions.Services.ValidationServices;
using Domain.Models;
using Infrastructure.Options;
using IntegrationTests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MongoDb;
using Testcontainers.PostgreSql;
using Program = Presentation.Program;

namespace IntegrationTests.Fixtures;

public class ControllersAppFactoryFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _database = new PostgreSqlBuilder().Build();
    private readonly MongoDbContainer _mongoDb = new MongoDbBuilder()
        .WithImage("mongo:latest").Build();
    public async Task InitializeAsync()
    {
        await _database.StartAsync();
        await _mongoDb.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _database.DisposeAsync();
        await _mongoDb.DisposeAsync();
        await base.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Utilities.SeedTestEnvironmentVariables(_database.GetConnectionString());
        _mongoDb.GetConnectionString();
        base.ConfigureWebHost(builder);

        var mongoOptions = new MongoOptions()
        {
            ConnectionString = _mongoDb.GetConnectionString(),
            DatabaseName = "test",
            CollectionsNames = new List<string>()
            {
                "offices"
            }
        };
        
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(mongoOptions);
        });
        
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IGoogleMapsApiClient>();
            services.RemoveAll<IPhoneValidatorService>();
            services.AddScoped<IGoogleMapsApiClient, GoogleMapsApiClientMock>();
            services.AddScoped<IPhoneValidatorService, PhoneValidatorServiceMock>();
        });
    }
}