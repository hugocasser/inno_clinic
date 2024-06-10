using BusinessLayer.Abstractions.Services;
using BusinessLayer.BackgroundJobs;
using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BusinessLayer;

public static class BusinessLayerInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddServices();
        
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFilesService, FilesService>();
        services.AddScoped<IFilesCleanerService, FilesCleanerService>();

        return services;
    }
    
    private static IServiceCollection AddJobs(this IServiceCollection services)
    {
        services
            .AddCleaningJobs();
        
        return services;
    }

    private static IServiceCollection AddCleaningJobs(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(FileCleaningBackgroundJob));
            configure
                .AddJob<FileCleaningBackgroundJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInSeconds(60).RepeatForever())); 
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        
        return services;
    }
}