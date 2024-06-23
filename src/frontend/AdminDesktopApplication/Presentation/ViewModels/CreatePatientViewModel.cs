using Application.Abstractions.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Abstractions.Services;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Pages.Profiles.Patients;

namespace Presentation.ViewModels;

[QueryProperty(nameof(CreatePatientModel), "CreatePatientModel")]
public partial class CreatePatientViewModel
    (IProfilesService profilesService,
        ICredentialsService credentialsService) : ObservableObject
{
    [ObservableProperty] private CreatePatientModel _createPatientModel = new CreatePatientModel();
    
    [RelayCommand]
    private async Task CreateProfileAsync()
    {
        var request = CreatePatientModel.MapToRequest();
        var requestResult = await profilesService.CreatePatientProfileAsync(request);

        if (!requestResult.IsSuccess)
        {
            switch (requestResult.GetResultData<string>())
            {
                case "Email already exists":
                {
                    await Shell.Current.DisplayAlert("Error", "Email already exists", "Ok");

                    break;
                }
                case "Something went wrong":
                {
                    await Shell.Current.DisplayAlert("Error", "Something went wrong", "Ok");

                    break;
                }
                default:
                {
                    var refreshResult = await credentialsService.TryRefreshTokenAsync();
                    
                    if (!refreshResult.IsSuccess)
                    {
                        await Shell.Current.DisplayAlert("Error", "Something went wrong", "Ok");
                        await Shell.Current.GoToAsync(nameof(LoginPage));
                        
                        return;
                    }
                    
                    requestResult = await profilesService.CreatePatientProfileAsync(request);

                    if (!requestResult.IsSuccess)
                    {
                        await Shell.Current.DisplayAlert("Error", "Email already exists", "Ok");
                        
                        return;
                    }
                    
                    break;
                }
            }
        }
        
        await Shell.Current.DisplayAlert("Success", "Profile created successfully", "Ok");
        await Shell.Current.GoToAsync(nameof(PatientPage));
    }
}