using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Presentation.Abstractions.Services;
using Presentation.Pages;
using Presentation.Pages.Profiles.Doctors;
using Presentation.Pages.Profiles.Patients;
using Presentation.Pages.Profiles.Receptionists;
using Presentation.Services;
using Application;
using CommunityToolkit.Maui;
using Infrastructure;

namespace Presentation.Extensions;

public static class ProgramExtension
{
    public static MauiAppBuilder ConfigureAppBuilder(this MauiAppBuilder builder)
    {
        builder.UseMauiCommunityToolkit();
        builder.UseMauiApp<App>();
        builder.ConfigureFonts();
        
        builder.Services
            .AddServices()
            .AddApplication()
            .AddInfrastructure()
            .AddMvvm();
        
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        RegisterRoutes();
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
            .AddSingleton<ICredentialsService, CredentialsService>()
            .AddSingleton<IStatusesMapperService, StatusesMapperService>()
            .AddSingleton<ISpecializationsMapperService, SpecializationsMapperService>();
        
        return services;
    }
    private static void RegisterRoutes() 
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
        Routing.RegisterRoute(nameof(ReceptionistCreationPage), typeof(ReceptionistCreationPage));
        Routing.RegisterRoute(nameof(PatientCreationPage), typeof(PatientCreationPage));
        Routing.RegisterRoute(nameof(DoctorCreationPage), typeof(DoctorCreationPage));
    }
}