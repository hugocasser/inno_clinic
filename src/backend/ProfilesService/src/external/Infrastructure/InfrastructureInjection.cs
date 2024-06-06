using System.Reflection;
using Application.Abstractions.Repositories.Outbox;
using Application.Abstractions.Repositories.Read;
using Application.Abstractions.Repositories.Write;
using Application.Common;
using Infrastructure.Interceptors;
using Infrastructure.Options;
using Infrastructure.Persistence.Read;
using Infrastructure.Persistence.Read.Repositories;
using Infrastructure.Persistence.Write;
using Infrastructure.Persistence.Write.Repositories;
using Infrastructure.Services;
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
            .AddReadRepositories()
            .AddWriteRepositories()
            .AddReadDbContext()
            .AddWriteDbContext()
            .AddOptions();
    }

    private static IServiceCollection AddReadRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReadDoctorsRepository, ReadDoctorsRepository>();
        services.AddScoped<IReadPatientsRepository, ReadPatientsRepository>();
        services.AddScoped<IReadReceptionistsRepository, ReadReceptionistsRepository>();

        return services;
    }

    private static IServiceCollection AddWriteRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWritePatientsRepository, WritePatientsRepository>();
        services.AddScoped<IWriteReceptionistsRepository, WriteReceptionistsRepository>();
        services.AddScoped<IWriteDoctorsRepository, WriteDoctorsRepository>();
        services.AddScoped<IOutboxMessagesRepository, OutboxMessagesRepository>();
        services.AddScoped<IStatusesRepository, StatusesRepository>();

        return services;
    }

    private static IServiceCollection AddWriteDbContext(this IServiceCollection services)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (string.IsNullOrEmpty(environment))
        {
            var exception = new ArgumentNullException
            {
                HelpLink = Assembly.GetExecutingAssembly().FullName,
                HResult = -1,
                Source = Assembly.GetExecutingAssembly().FullName
            };

            throw exception;
        }

        services
            .AddDbContext<ProfilesWriteDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<PostgresOptions>>();
                options
                    .UseNpgsql(dbOptions.Value.ConnectionString, config =>
                        {
                            config.MigrationsAssembly("Migrations");
                        })
                    .AddInterceptors(new ConvertDomainEventsToOutboxMessagesInterceptor());


                if (environment is "Test" or "Development")
                {
                    options
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                }
            });

        return services;
    }

    private static IServiceCollection AddReadDbContext(this IServiceCollection services)
    {
        services.AddScoped<ProfilesReadDbContext>();
        services.AddScoped<IMongoClient, CustomMongoClient>();

        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        services.AddGenericOptions<PostgresOptions>(nameof(PostgresOptions));
        services.AddGenericOptions<MongoOptions>(nameof(MongoOptions));
        
        return services;
    }
}