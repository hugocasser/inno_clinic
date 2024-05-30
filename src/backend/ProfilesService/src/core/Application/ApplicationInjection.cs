using System.Reflection;
using Application.Abstractions.CQRS;
using Application.Abstractions.Services;
using Application.Abstractions.Services.ExternalServices;
using Application.Services;
using Application.Services.CQRS;
using Application.Services.External;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddCqrs()
            .AddValidators();
    }
    
    private static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddRequestHandlersFromAssembly(Assembly.GetExecutingAssembly());
        services.AddPipelineBehaviorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IRequestSender, RequestSender>();
        
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordGeneratorService, PasswordGeneratorService>();
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOfficesService, OfficesService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<ISpecializationsService, SpecializationsService>();
        
        return services;
    }
}