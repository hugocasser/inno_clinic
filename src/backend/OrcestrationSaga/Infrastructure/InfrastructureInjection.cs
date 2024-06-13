using Application.Abstractions.Services.External;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddExternalServices();
        
        return services;
    }
    
    
    private static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IFilesService, FilesService>()
            .AddScoped<IOfficesService, OfficesService>()
            .AddScoped<IProfilesService, ProfilesService>();
        
        return services;
    }
}