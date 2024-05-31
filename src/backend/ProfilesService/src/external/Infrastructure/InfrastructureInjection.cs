using System.Reflection;
using Application.Abstractions.Repositories.Read;
using Application.Abstractions.Repositories.Write;
using Infrastructure.Interceptors;
using Infrastructure.Persistence.Read;
using Infrastructure.Persistence.Read.Repositories;
using Infrastructure.Persistence.Write;
using Infrastructure.Persistence.Write.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            .AddWriteDbContext();
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
            .AddDbContext<ProfilesWriteDbContext>(options =>
            {
                options
                    .UseNpgsql()
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
}