using System.Reflection;
using System.Threading.RateLimiting;
using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ExternalServices;
using Application.Abstractions.Services.TransactionalOutboxServices;
using Application.Abstractions.Services.ValidationServices;
using Application.BackgroundJobs;
using Application.Common;
using Application.Options;
using Application.Services.ExternalServices;
using Application.Services.TransactionalOutboxServices;
using Application.Services.ValidationServices;
using Application.Services.ValidationServices.DecoratedServicesWithRateLimiting;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Polly;
using Polly.Retry;
using Quartz;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddResiliencePipeline()
            .AddOptions()
            .AddJobs()
            .AddValidators()
            .AddPipelineBehaviors()
            .AddHandlers();
        
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
            .AddGenericOptions<PhoneValidatorOptions>(nameof(PhoneValidatorOptions))
            .AddGenericOptions<GoogleApiRpsOptions>(nameof(GoogleApiRpsOptions))
            .AddGenericOptions<PhoneValidatorRpsOptions>(nameof(PhoneValidatorRpsOptions));
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
                var options = pollyContext
                    .GetOptions<PhoneValidatorRpsOptions>();

                builder
                    .ConfigureTelemetry(NullLoggerFactory.Instance)
                    .AddRateLimiter(new FixedWindowRateLimiter(
                        new FixedWindowRateLimiterOptions()
                        {
                            PermitLimit = options.RequestsPerSecond,
                        }));
            } );
        
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        return services;
    }
    
    private static IServiceCollection AddJobs(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInSeconds(60).RepeatForever())); 
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        
        return services;
    }
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
    
    private static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
    {
        return services;
    }
}