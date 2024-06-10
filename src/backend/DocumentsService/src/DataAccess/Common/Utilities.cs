using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Common;

public static class Utilities
{
    public static IServiceCollection AddGenericOptions<T>(this IServiceCollection services) where T : class
    {
        services
            .AddOptions<T>()
            .BindConfiguration(typeof(T).Name)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services;
    }
}