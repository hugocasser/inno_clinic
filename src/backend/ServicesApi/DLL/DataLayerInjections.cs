using DLL.Common;
using DLL.Options;
using Microsoft.Extensions.DependencyInjection;

namespace DLL;

public static class DataLayerInjections
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services)
    {
        return services
            .AddOptions();
    }

    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        return services
            .AddGenericOptions<PostgresOptions>();
    }
}