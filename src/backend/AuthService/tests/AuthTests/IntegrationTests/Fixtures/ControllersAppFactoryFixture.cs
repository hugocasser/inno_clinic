using Application.Abstractions.Email;
using AuthTests.IntegrationTests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;
using Testcontainers.Redis;
using Program = Presentation.Program;

namespace AuthTests.IntegrationTests.Fixtures;

public class ControllersAppFactoryFixture :  WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _mssql = new MsSqlBuilder().WithImage("mcr.microsoft.com/mssql/server:2022-latest").Build();
    private readonly RedisContainer _redisContainer = new RedisBuilder().Build();
    
    public async Task InitializeAsync()
    {
        await _mssql.StartAsync();
        await _redisContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _mssql.DisposeAsync();
        await _redisContainer.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        TestUtils.SeedEnvironmentVariables(_mssql.GetConnectionString(), _redisContainer.GetConnectionString());
        
        base.ConfigureWebHost(builder);        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<ISmtpClientService>();
            services.AddScoped<ISmtpClientService, SmtpClientServiceMock>();
        });
    }
}