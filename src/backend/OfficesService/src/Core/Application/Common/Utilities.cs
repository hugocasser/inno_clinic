using Microsoft.Extensions.DependencyInjection;

namespace Application.Common;

public static class Utilities
{
    public static IServiceCollection AddGenericOptions<T>(this IServiceCollection services, string sectionName) where T : class
    { 
        services
            .AddOptions<T>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}