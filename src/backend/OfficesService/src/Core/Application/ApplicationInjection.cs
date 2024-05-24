using System.Threading.RateLimiting;
using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ExternalServices;
using Application.Abstractions.Services.TransactionalOutboxServices;
using Application.Abstractions.Services.ValidationServices;
using Application.Common;
using Application.Options;
using Application.Services.ExternalServices;
using Application.Services.TransactionalOutboxServices;
using Application.Services.ValidationServices;
using Application.Services.ValidationServices.DecoratedServicesWithRateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Polly;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddResiliencePipeline();
        
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IDomainEventProcessorService, DomainEventProcessorService>()
            .AddScoped<IPhotoService, PhotoService>()
            .AddScoped<IGoogleMapsApiClient, GoogleMapsApiClient>()
            .AddScoped<IPhoneValidatorService, PhoneValidatorService>();
        
        services
            .Decorate<IGoogleMapsApiClient, DecoratedGoogleMapsApiClient>()
            .Decorate<IPhoneValidatorService, DecoratedPhoneValidatorService>();
        
        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        return services
            .AddGenericOptions<GoogleApiClientOptions>(nameof(GoogleApiClientOptions))
            .AddGenericOptions<PhoneValidatorOptions>(nameof(PhoneValidatorOptions));
    }
    
    private static IServiceCollection AddResiliencePipeline(this IServiceCollection services)
    {
        services.AddResiliencePipeline<string, IResult>(nameof(DecoratedGoogleMapsApiClient),
            (builder, pollyContext) =>
            {
                var allowedRps = pollyContext
                    .GetOptions<GoogleApiRpsOptions>()
                    .RequestsPerSecond;

                builder
                    .ConfigureTelemetry(NullLoggerFactory.Instance)
                    .AddRateLimiter(new FixedWindowRateLimiter(
                        new FixedWindowRateLimiterOptions()
                        {
                            PermitLimit = allowedRps,
                        }));
            } );
        
        services.AddResiliencePipeline<string, IResult>(nameof(DecoratedPhoneValidatorService),
            (builder, pollyContext) =>
            {
                var allowedRps = pollyContext
                    .GetOptions<PhoneValidatorRpsOptions>()
                    .RequestsPerSecond;

                builder
                    .ConfigureTelemetry(NullLoggerFactory.Instance)
                    .AddRateLimiter(new FixedWindowRateLimiter(
                        new FixedWindowRateLimiterOptions()
                        {
                            PermitLimit = allowedRps,
                        }));
            } );
        
        return services;
    }
}