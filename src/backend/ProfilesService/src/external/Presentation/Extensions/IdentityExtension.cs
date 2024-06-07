namespace Presentation.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddAuthentication();
        
        return services;
    }
}