using Application.Abstractions.Persistence.Repositories;
using Application.Common;
using Infrastructure.Abstractions;
using Infrastructure.Interceptors;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddWriteContext()
            .AddReadContext()
            .AddRepositories()
            .AddOptions();
    }
    
    private static IServiceCollection AddWriteContext(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        
        if (!string.IsNullOrEmpty(connectionString))
        {
            return services
                .AddDbContext<IOfficesWriteDbContext, OfficesWriteDbContext>((serviceProvider, options) =>
                {
                    var tenantInterceptor = serviceProvider
                        .GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();
                    options.UseNpgsql(connectionString, npgsqlOptions =>
                        {
                            npgsqlOptions.MigrationsAssembly("PostgresMigrations");
                        })
                        .AddInterceptors(tenantInterceptor);
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                });
        }

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        
        return services
            .AddDbContext<IOfficesWriteDbContext, OfficesWriteDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<PostgresOptions>>();
                var tenantInterceptor = serviceProvider
                    .GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();
                options.UseNpgsql(dbOptions.Value.ConnectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("PostgresMigrations");
                })
                    .AddInterceptors(tenantInterceptor);

                if (environment != "Development")
                {
                    return;
                }

                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });
    }
    
    private static IServiceCollection AddReadContext(this IServiceCollection services)
    {
        services.AddScoped<IMongoClient, CustomMongoClient>();
        services.AddScoped<IOfficesReadDbContext, OfficesReadDbContext>();

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IWriteOfficesRepository, WriteOfficesRepository>()
            .AddScoped<IReadOfficesRepository, ReadOfficesRepository>();
    }

    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        return services
            .AddGenericOptions<PostgresOptions>(nameof(PostgresOptions))
            .AddGenericOptions<MongoOptions>(nameof(MongoOptions));
    }
}