using DataAccess.Abstractions;
using DataAccess.Common;
using DataAccess.Options;
using DataAccess.Storages;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class DataAccessInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services
            .AddOptions()
            .AddServices();
        
        return services;
    }
    
    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        services.AddGenericOptions<AzureOptions>();
        services.AddOptions<BlobOptions>();
        
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IBlobService, BlobService>();
        
        return services;
    }
}