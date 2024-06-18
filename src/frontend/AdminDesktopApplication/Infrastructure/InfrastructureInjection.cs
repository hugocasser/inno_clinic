using Application.Abstractions.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddExternalServices();
        
        return services;
    }
    
    private static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped<IProfilesService, ProfilesService>();
        
        return services;
    }
}