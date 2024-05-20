using System.Reflection;
using Application.Abstractions.Auth;
using Application.Abstractions.Cache;
using Application.Abstractions.Email;
using Application.Abstractions.Services;
using Application.Common;
using Application.Options;
using Application.PipelineBehaviors;
using Application.Services.Auth;
using Application.Services.Cache;
using Application.Services.Email;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddServices()
            .AddPipelineBehaviors()
            .AddValidators()
            .AddOptions()
            .AddRequestHandlers();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserService, UsersService>()
            .AddScoped<IAccessTokensService, AccessTokensService>()
            .AddScoped<IEmailSenderService, EmailSenderService>()
            .AddScoped<IRefreshTokensService, RefreshTokenService>()
            .AddScoped<IHttpContextAccessorExtension, HttpContextAccessorExtension>()
            .AddScoped<ICacheService, CacheService>();
    }

    private static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        return services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        services
            .AddGenericOptions<AccessTokenOptions>()
            .AddGenericOptions<EmailSenderOptions>()
            .AddGenericOptions<RedisOptions>();
        
        return services;
    }
    private static IServiceCollection AddRequestHandlers(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}