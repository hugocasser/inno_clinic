using Application.Abstractions.Services;
using Application.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Common;
using Presentation.Models;
using Presentation.Pages.Profiles.Receptionists;
using Presentation.Resources;

namespace Presentation.ViewModels;

[QueryProperty(nameof(ReceptionistModel), "ReceptionistModel")]
public partial class ReceptionistViewModel(IProfilesService profilesService) : ObservableObject
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
            await Shell.Current.GoToAsync(nameof(ReceptionistsPage));
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
                await Shell.Current
                    .DisplayAlert(InformMessages.Error, "", InformMessages.Ok);
            }
            
            await Shell.Current.GoToAsync(nameof(ReceptionistsPage));
        }
    }
}