using System.Reflection;
using Application.Abstractions.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.DomainEvents;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddEventHandlersFromAssembly
        (this IServiceCollection services, Assembly assembly)
    {
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface)
            .ToList();

        foreach (var serviceType in serviceTypes.Where(serviceType => serviceType
                     .GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>))))
        {
            var myInterface = serviceType.GetInterfaces().First(type => type.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));
            services.AddScoped(myInterface,serviceType);
        }

        return services;
    }
}