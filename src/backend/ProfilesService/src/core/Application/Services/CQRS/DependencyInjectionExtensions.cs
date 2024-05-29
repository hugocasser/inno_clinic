using System.Reflection;
using Application.Abstractions.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.CQRS;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddRequestHandlersFromAssembly
        (this IServiceCollection services, Assembly assembly)
    {
        foreach (var handler in assembly.GetTypes()
                     .Where(type => type.IsClass && type.IsAssignableTo(typeof(IRequestHandler<,>))))
        {
            var requestTypes = handler
                .GetInterfaces()
                .Where(type => type.IsAssignableTo(typeof(IRequestHandler<,>)));

            foreach (var requestType in requestTypes)
            {
                services.AddScoped(requestType, handler);
            }
        }

        return services;
    }

    public static IServiceCollection AddPipelineBehaviorsFromAssembly
        (this IServiceCollection services, Assembly assembly)
    {
        foreach (var behavior in assembly.GetTypes()
                     .Where(type => type.IsClass && type.IsAssignableTo(typeof(IPipelineBehavior<,>))))
        {
            var behaviorTypes = behavior
                .GetInterfaces()
                .Where(type => type.IsAssignableTo(typeof(IPipelineBehavior<,>)));

            foreach (var behaviorType in behaviorTypes)
            {
                services.AddScoped(behaviorType, behavior);
            }
        }

        return services;
    }
}