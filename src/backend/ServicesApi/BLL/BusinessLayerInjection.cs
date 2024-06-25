using BLL.Abstractions.Services;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL;

public static class BusinessLayerInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        return services
            .AddServices();
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IServicesService, ServicesService>()
            .AddScoped<ISpecializationsService, SpecializationsService>();
    }
}