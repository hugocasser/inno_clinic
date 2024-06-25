using Application.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Abstractions.Services;
using Presentation.Abstractions.Services.PipelinedService;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Pages.Profiles.Patients;
using Presentation.Resources;

namespace Presentation.ViewModels;

[QueryProperty(nameof(CreatePatientModel), "CreatePatientModel")]
public partial class CreatePatientViewModel
    (IPipelinedProfilesService profilesService,
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
                case TextResponses.EmailAlreadyExists:
                {
                    await Shell.Current.DisplayAlert
                        ( InformMessages.Error, InformMessages.EmailAlreadyExists, InformMessages.Ok);

                    break;
                }
                case TextResponses.SomethingWentWrong:
                {
                    await Shell.Current.DisplayAlert
                        (InformMessages.Error, InformMessages.SomethingWentWrong, InformMessages.Ok);

                    break;
                }
                case TextResponses.Unauthorized:
                {
                    await credentialsService.LogoutAsync();
                    
                    await Shell.Current.DisplayAlert(InformMessages.Error, InformMessages.Unathorized, InformMessages.Ok);
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                    
                    break;
                }
            }
        }
        
        await Shell.Current.DisplayAlert
            (InformMessages.Success, InformMessages.ProfileCreated, InformMessages.Ok);
        await Shell.Current.GoToAsync(nameof(PatientPage));
    }
}