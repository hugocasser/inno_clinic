using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Presentation.Abstractions.Services;
using Presentation.Components.Menu;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Pages.Profiles.Doctors;
using Presentation.Pages.Profiles.Patients;
using Presentation.Pages.Profiles.Receptionists;
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
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(DoctorsPage), typeof(DoctorsPage));
        Routing.RegisterRoute(nameof(DoctorPage), typeof(DoctorPage));
        Routing.RegisterRoute(nameof(PatientsPage), typeof(PatientsPage));
        Routing.RegisterRoute(nameof(PatientPage), typeof(PatientPage));
        Routing.RegisterRoute(nameof(ReceptionistsPage), typeof(ReceptionistsPage));
        Routing.RegisterRoute(nameof(ReceptionistPage), typeof(ReceptionistPage));
    }
    
    private static IServiceCollection ConfigurePages(this IServiceCollection services)
    {
        services
            .AddTransient<MainPage>()
            .AddTransient<LoginPage>()
            .AddTransient<SettingsPage>()
            .AddTransient<MenuComponent>()
            .AddTransient<DoctorsPage>()
            .AddTransient<DoctorPage>()
            .AddTransient<PatientPage>()
            .AddTransient<PatientsPage>()
            .AddTransient<ReceptionistPage>()
            .AddTransient<ReceptionistsPage>();
        
        return services;
    }
    
    private static IServiceCollection AddModels(this IServiceCollection services)
    {
        services
            .AddTransient<LoginModel>()
            .AddTransient<PatientListItem>();
        
        return services;
    }
    
    private static void AddViewModels(this IServiceCollection services)
    {
        services
            .AddTransient<LoginViewModel>()
            .AddTransient<MenuViewModel>()
            .AddTransient<PatientListItemViewModel>();
    }
}