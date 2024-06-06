using System.Reflection;
using Application.Abstractions.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.CQRS;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddRequestHandlersFromAssembly
        (this IServiceCollection services, Assembly assembly)
    {
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface)
            .ToList();

        foreach (var serviceType in serviceTypes.Where(serviceType => serviceType
                     .GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))))
        {
            var myInterface = serviceType.GetInterfaces().First(type => type.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
            services.AddScoped(myInterface,serviceType);
        }

        return services;
    }

    public static IServiceCollection AddPipelineBehaviorsFromAssembly
        (this IServiceCollection services, Assembly assembly)
    {
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface)
            .ToList();

        foreach (var serviceType in serviceTypes.Where(serviceType => serviceType.GetInterfaces()
                     .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>))))
        {
            var myInterface = serviceType.GetInterfaces().First(type => type.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>));
            services.AddScoped(myInterface,serviceType);
        }

        return services;
    }
}