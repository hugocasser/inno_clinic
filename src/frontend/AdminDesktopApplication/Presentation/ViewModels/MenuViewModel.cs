using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Pages;
using Presentation.Pages.Profiles.Doctors;
using Presentation.Pages.Profiles.Patients;
using Presentation.Pages.Profiles.Receptionists;

namespace Presentation.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isMenuVisible = true;
    
    [RelayCommand]
    private async Task NavigateToDoctorsPage()
    {
        await Shell.Current.GoToAsync(nameof(DoctorsPage), true);
    }
    
    [RelayCommand]
    private async Task NavigateToReceptionistsPage()
    {
        await Shell.Current.GoToAsync(nameof(ReceptionistsPage), true);
    }
    
    [RelayCommand]
    private async Task NavigateToSettingsPage()
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage), true);
    }
    
    [RelayCommand]
    private async Task Logout()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage), true);
    }
    
    [RelayCommand]
    private async Task NavigateToPatientsPage()
    {
        await Shell.Current.GoToAsync(nameof(PatientsPage), true);
    }
    
    [RelayCommand]
    private void ToggleMenu()
    {
        IsMenuVisible = !IsMenuVisible;
    }
}