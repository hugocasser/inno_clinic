using System.Reflection;
using Application.Abstractions.CQRS;
using Application.Abstractions.DomainEvents;
using Application.Abstractions.Http;
using Application.Abstractions.Services;
using Application.Abstractions.Services.ExternalServices;
using Application.Abstractions.TransactionalOutbox;
using Application.Services;
using Application.Services.CQRS;
using Application.Services.DomainEvents;
using Application.Services.External;
using Application.Services.Http;
using Application.Services.TransactionalOutbox;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(IRequestHandler<,>).Assembly;
        
        return services
            .AddCqrs(assembly)
            .AddValidators(assembly)
            .AddServices()
            .AddDomainEvents(assembly)
            .AddTransactionalOutbox();
    }
    
    private static IServiceCollection AddCqrs(this IServiceCollection services, Assembly assembly)
    {
        services.AddRequestHandlersFromAssembly(assembly);
        // services.AddPipelineBehaviorsFromAssembly(assembly);
        
        services.AddScoped<IRequestSender, RequestSender>();
        
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordGeneratorService, PasswordGeneratorService>();
        
        services.AddScoped<ISpecializationsService, SpecializationsService>();
        services.AddScoped<IHttpContextAccessorExtensions, HttpContextAccessorExtensions>();
        
        return services;
    }

    private static IServiceCollection AddDomainEvents(this IServiceCollection services, Assembly assembly)
    {
        services.AddEventHandlersFromAssembly(assembly);
        
        services.AddScoped<IDomainEventSender, DomainEventSender>();
        
        return services;
    }
    
    private static IServiceCollection AddTransactionalOutbox(this IServiceCollection services)
    {
        services.AddScoped<IOutboxMessageProcessor, OutboxMessageProcessor>();
        
        return services;
    }
}