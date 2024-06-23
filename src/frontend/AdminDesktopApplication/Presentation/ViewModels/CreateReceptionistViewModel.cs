using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Models;
using Presentation.Pages.Profiles.Receptionists;

namespace Presentation.ViewModels;

[QueryProperty(nameof(CreateReceptionistModel), "CreateReceptionistModel")]
public partial class CreateReceptionistViewModel : ObservableObject
{
    [RelayCommand]
    private async Task CreateProfileAsync()
    {
        await Shell.Current.GoToAsync(nameof(ReceptionistsPage));
    }
}