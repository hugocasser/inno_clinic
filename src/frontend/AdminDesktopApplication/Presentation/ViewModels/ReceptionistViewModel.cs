using Application.Dtos;
using Application.Dtos.Receptionist;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Abstractions.Services;
using Presentation.Abstractions.Services.PipelinedService;
using Presentation.Common;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Pages.Profiles.Receptionists;
using Presentation.Resources;

namespace Presentation.ViewModels;

[QueryProperty(nameof(ReceptionistModel), "ReceptionistModel")]
public partial class ReceptionistViewModel(
    IPipelinedProfilesService profilesService,
    ICredentialsService credentialsService) : ObservableObject
{
    [ObservableProperty] private ReceptionistModel _receptionist = null!;
    [ObservableProperty] private bool _isMiddleNameVisible;
    [ObservableProperty] private string _fullName = null!;

    public async Task InitializeAsync(Guid id)
    {
        var requestResult = await profilesService.GetReceptionistProfileAsync(id);
        
        await Utilities.CheckResultAndShowAlertWhenFails<ReceptionistViewDto>
            (requestResult, InformMessages.ProfileNotFound, out var patient);
        
        if (patient is null)
        {
            if (Utilities.IsUnauthorized)
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
                return;
            }
            
            await Shell.Current.GoToAsync(nameof(ReceptionistsPage));
            await credentialsService.LogoutAsync();
            
            return;
        }
        
        var model = ReceptionistModel.MapFromResponse(patient);
        
        FullName = NavigationResources.Receptionist + $": {model.FirstName} {model.LastName}";

        IsMiddleNameVisible = model.MiddleName is not null;
        
        Receptionist = model;
    }

    [RelayCommand]
    private async Task Delete()
    {
        var result = await Shell.Current
            .DisplayAlert(InformMessages.AreYouSure,
                InformMessages.WontCanRollbackProfile,
                InformMessages.Continue, InformMessages.Back);
        
        if (result)
        {
            var requestResult = await profilesService.DeleteProfileAsync(Receptionist.Id);
            
            if (!requestResult.IsSuccess)
            {
                var error = requestResult.GetResultData<string>();
                
                if (string.IsNullOrEmpty(error))
                {
                    await Shell.Current
                        .DisplayAlert(InformMessages.Error, error, InformMessages.Ok);
                    return;
                }
                if (error == TextResponses.Unauthorized)
                {
                    Utilities.IsUnauthorized = true;
                    
                    await Shell.Current.DisplayAlert
                        (InformMessages.Error, InformMessages.Unathorized, InformMessages.Ok);
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                    
                    return;
                }
                
                await Shell.Current
                    .DisplayAlert(InformMessages.Error, InformMessages.SomethingWentWrong, InformMessages.Ok);
            }
            
            await Shell.Current.DisplayAlert(InformMessages.Success, InformMessages.Success, InformMessages.Ok);
            await Shell.Current.GoToAsync(nameof(ReceptionistsPage));
        }
    }
}