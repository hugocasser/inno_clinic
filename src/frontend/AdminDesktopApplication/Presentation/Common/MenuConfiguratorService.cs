using System.Collections.ObjectModel;
using System.Windows.Input;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Pages.Profiles.Doctors;
using Presentation.Pages.Profiles.Patients;
using Presentation.Pages.Profiles.Receptionists;
using Presentation.Resources;

namespace Presentation.Common;

public static class MenuConfiguratorService
{
    
    public static ObservableCollection<CustomMenuItem> Configure(string role)
    {
        var collection = new ObservableCollection<CustomMenuItem>
        {
            new() {Name = NavigationResources.Patients, Command = new Command(NavigateToPatientsPage) },
            new() {Name = NavigationResources.Receptionists, Command = new Command(NavigateToReceptionistsPage) },
            new() {Name = NavigationResources.Doctors, Command = new Command(NavigateToDoctorsPage)},
            new() {Name = NavigationResources.Settings, Command = new Command(NavigateToSettingsPage)},
            new() {Name = NavigationResources.Logout, Command = new Command(Logout)}
        };

        return collection;
    }

    private static async void NavigateToPatientsPage()
    {
        await Shell.Current.GoToAsync(nameof(PatientsPage), true);
    }
    
    private static async void NavigateToReceptionistsPage()
    {
        await Shell.Current.GoToAsync(nameof(ReceptionistsPage), true);
    }
    
    private static async void NavigateToSettingsPage()
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage), true);
    }
    
    private static async void NavigateToDoctorsPage()
    {
        await Shell.Current.GoToAsync(nameof(DoctorsPage), true);
    }
    
    private static async void Logout()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage), true);
    }
}