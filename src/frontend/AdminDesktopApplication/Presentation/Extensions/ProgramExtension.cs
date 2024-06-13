using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Presentation.Abstractions.Services;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Services;
using Presentation.ViewModels;

namespace Presentation.Extensions;

public static class ProgramExtension
{
    public static MauiAppBuilder ConfigureAppBuilder(this MauiAppBuilder builder)
    {
        builder.UseMauiApp<App>();
        builder.ConfigureFonts();
        
        builder.Services
            .ConfigurePages()
            .AddServices()
            .AddModels()
            .AddViewModels();

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        ConfigureRouting();
        
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder;
    }

    private static void ConfigureFonts(this MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("JetBrainsMono-Bold.ttf", "JbBoldMono");
                fonts.AddFont("JetBrainsMono-Medium.ttf", "JbMediumMono");
                fonts.AddFont("JetBrainsMono-Regular.ttf", "JetBrainsMono");
            });
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ICredentialsService, CredentialsService>();
        
        return services;
    }
    private static void ConfigureRouting()
    {
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
    }
    
    private static IServiceCollection ConfigurePages(this IServiceCollection services)
    {
        services
            .AddTransient<MainPage>()
            .AddTransient<LoginPage>()
            .AddTransient<RegisterPage>();
        
        return services;
    }
    
    private static IServiceCollection AddModels(this IServiceCollection services)
    {
        services
            .AddSingleton<LoginModel>();
        
        return services;
    }
    
    private static void AddViewModels(this IServiceCollection services)
    {
        services
            .AddTransient<LoginViewModel>();
    }
}