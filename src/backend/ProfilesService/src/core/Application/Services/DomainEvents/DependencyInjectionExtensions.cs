using System.Reflection;
using Application.Abstractions.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.DomainEvents;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDomainEventHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var handlers = assembly
            .GetTypes()
            .Where(type => type.IsClass && type.IsAssignableTo(typeof(IDomainEventHandler<>))).ToList();
        
        handlers.ForEach(handler =>
        {
            var enumerable = assembly.GetTypes()
                .Where(type => type.IsInterface && type.IsAssignableTo(typeof(IDomainEventHandler<>)) &&
                    handler.IsAssignableFrom(type));

            foreach (var type in enumerable)
            {
                services.AddScoped(type, handler);
            }
        });
        
        return services;
    }
}