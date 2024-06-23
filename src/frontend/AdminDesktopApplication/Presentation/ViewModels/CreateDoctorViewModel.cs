using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Models;
using Presentation.Pages.Profiles.Doctors;

namespace Presentation.ViewModels;

[QueryProperty(nameof(CreateDoctorModel), "CreateDoctorModel")]
public partial class CreateDoctorViewModel : ObservableObject
{
    [RelayCommand]
    private async Task CreateProfileAsync()
    {
        await Shell.Current.GoToAsync(nameof(DoctorPage));
    }
}