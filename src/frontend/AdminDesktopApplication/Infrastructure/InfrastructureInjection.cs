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
        services.AddSingleton<IProfilesService, ProfilesService>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IOfficesService, OfficesService>();
        services.AddSingleton<ISpecializationsService, SpecializationsService>();
        services.AddSingleton<IStatusesService, StatusesService>();
        
        return services;
    }
}