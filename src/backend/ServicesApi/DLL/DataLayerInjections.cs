using DLL.Abstractions.Persistence.Repositories;
using DLL.Common;
using DLL.Options;
using DLL.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DLL;

public static class DataLayerInjections
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services)
    {
        return services
            .AddOptions()
            .AddRepositories();
    }

    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        return services
            .AddGenericOptions<PostgresOptions>();
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ICategoriesRepository, CategoriesRepository>()
            .AddScoped<IServicesRepository, ServicesRepository>()
            .AddScoped<ISpecializationsRepository, SpecializationsRepository>();
    }
}