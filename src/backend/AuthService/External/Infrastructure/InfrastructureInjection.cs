using Application.Abstractions.Repositories;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string environment)
    {
        return services
            .AddDbContext(configuration, environment)
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration, string environment)
    {
        var databaseOptions = new DatabaseOptions();
        configuration.GetSection(nameof(DatabaseOptions)).Bind(databaseOptions);

        services
            .AddDbContext<AuthDbContext>(options =>
            {
                switch (environment)
                {
                    case "Development":
                    {
                        options.UseSqlite(databaseOptions.ConnectionString)
                            .EnableSensitiveDataLogging();
                        
                        break;
                    }
                    case "Container":
                    {
                        options.UseSqlServer(databaseOptions.ConnectionString);

                        break;
                    }
                    default:
                    {
                        throw new Exception("Environment variable ASPNETCORE_ENVIRONMENT is not set.");
                    }
                }
            });

        return services;
    }
}