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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using MicrosoftOptions = Microsoft.Extensions.Options.Options;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddServices(configuration)
            .AddPipelineBehaviors()
            .AddValidators()
            .AddOptions()
            .AddRequestHandlers();
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IUserService, UsersService>()
            .AddScoped<IAccessTokensService, AccessTokensService>()
            .AddScoped<IEmailSenderService, EmailSenderService>()
            .AddScoped<IRefreshTokensService, RefreshTokenService>()
            .AddScoped<IHttpContextAccessorExtension, HttpContextAccessorExtension>()
            .AddScoped<ICacheService, CacheService>()
            .AddScoped<IConfirmMessageSenderService, ConfirmMessageSenderService>()
            .AddScoped<ISmtpClientService, SmtpClientService>()
            .AddScoped<ICacheService, CacheService>()
            .AddRedisCache(configuration);
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
            .AddGenericOptions<AccessTokenOptions>(nameof(AccessTokenOptions))
            .AddGenericOptions<EmailSenderOptions>(nameof(EmailSenderOptions))
            .AddGenericOptions<RedisOptions>(nameof(RedisOptions));
        
        return services;
    }
    private static IServiceCollection AddRequestHandlers(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        return services;
    }
    
    private static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisOptions = new RedisOptions();
        var redisConnectionString = Environment.GetEnvironmentVariable("Test_RedisConnectionString");
        if (redisConnectionString != null)
        {
            redisOptions.RefreshTokenConnectionString = redisConnectionString;
        }
        else
        {
            configuration.GetSection(nameof(RedisOptions)).Bind(redisOptions);
            services.AddSingleton(MicrosoftOptions.Create(redisOptions));
        }
        
        services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(provider => 
            provider.GetService<ConnectionMultiplexer>() 
            ?? ConnectionMultiplexer.Connect(redisOptions.RefreshTokenConnectionString));
        
        return services;
    }
}