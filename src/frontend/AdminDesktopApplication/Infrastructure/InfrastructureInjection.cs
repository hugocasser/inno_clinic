using Application.Abstractions.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddExternalServices();
    }
    
    private static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped<IProfilesService, ProfilesService>();
        
        return services;
    }
}