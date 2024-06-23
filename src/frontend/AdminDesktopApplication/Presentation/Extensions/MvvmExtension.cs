using Presentation.Components.Menu;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Pages.Profiles.Doctors;
using Presentation.Pages.Profiles.Patients;
using Presentation.Pages.Profiles.Receptionists;
using Presentation.ViewModels;

namespace Presentation.Extensions;

public static class MvvmExtension
{
    public static void AddMvvm(this IServiceCollection services)
    {
       services
            .AddViews()
            .AddModels()
            .AddViewModels();
    }

    private static IServiceCollection AddViews(this IServiceCollection services)
    {
        services
            .AddTransient<MainPage>()
            .AddTransient<LoginPage>()
            .AddTransient<SettingsPage>()
            .AddTransient<MenuComponent>()
            .AddTransient<DoctorsPage>()
            .AddTransient<DoctorPage>()
            .AddTransient<PatientsPage>()
            .AddTransient<PatientPage>()
            .AddTransient<ReceptionistPage>()
            .AddTransient<ReceptionistsPage>()
            .AddTransient<ReceptionistCreationPage>()
            .AddTransient<PatientCreationPage>()
            .AddTransient<DoctorCreationPage>();

        return services;
    }

    private static IServiceCollection AddModels(this IServiceCollection services)
    {
        services
            .AddTransient<LoginModel>()
            .AddTransient<PatientListItem>()
            .AddTransient<DoctorListItem>()
            .AddTransient<CustomMenuItem>()
            .AddTransient<ReceptionistListItem>()
            .AddTransient<DoctorModel>()
            .AddTransient<PatientModel>()
            .AddTransient<ReceptionistModel>()
            .AddTransient<CreateDoctorModel>()
            .AddTransient<CreatePatientModel>()
            .AddTransient<CreateReceptionistModel>();

        return services;
    }

    private static void AddViewModels(this IServiceCollection services)
    {
        services
            .AddTransient<LoginViewModel>()
            .AddTransient<MenuViewModel>()
            .AddTransient<PatientListItemViewModel>()
            .AddTransient<DoctorListItemViewModel>()
            .AddTransient<ReceptionistListItemViewModel>()
            .AddTransient<DoctorViewModel>()
            .AddTransient<PatientViewModel>()
            .AddTransient<ReceptionistViewModel>()
            .AddTransient<CreateReceptionistViewModel>()
            .AddTransient<CreateDoctorViewModel>()
            .AddTransient<CreatePatientViewModel>();
    }
}