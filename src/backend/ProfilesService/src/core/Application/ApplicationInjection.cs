using System.Reflection;
using Application.Abstractions.CQRS;
using Application.Services.CQRS;
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
}