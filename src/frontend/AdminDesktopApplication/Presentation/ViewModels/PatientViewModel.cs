using Application.Abstractions.Services;
using Application.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Common;
using Presentation.Models;
using Presentation.Pages.Profiles.Patients;
using Presentation.Resources;

namespace Presentation.ViewModels;

[QueryProperty(nameof(PatientModel), "PatientModel")]
public partial class PatientViewModel(IProfilesService profilesService) : ObservableObject
{
    [ObservableProperty] private PatientModel _patient = null!;
    [ObservableProperty] private bool _isMiddleNameVisible;
    [ObservableProperty] private string _fullName = null!;

    public async Task InitializeAsync(Guid id)
    {
        var requestResult = await profilesService.GetPatientProfileAsync(id);
        
        await Utilities.CheckResultAndShowAlertWhenFails<PatientViewDto>
            (requestResult, InformMessages.ProfileNotFound, out var patient);
        
        if (patient is null)
        {
            await Shell.Current.GoToAsync(nameof(PatientsPage));
            return;
        }
        
        var model = PatientModel.MapFromResponse(patient);
        
        FullName = NavigationResources.Patient  +$": {model.FirstName} {model.LastName}";

        IsMiddleNameVisible = model.MiddleName is not null;
        
        Patient = model;
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
            var requestResult = await profilesService.DeleteProfileAsync(Patient.Id);
            
            if (!requestResult.IsSuccess)
            {
                await Shell.Current
                    .DisplayAlert(InformMessages.Error, "", InformMessages.Ok);
            }
            
            await Shell.Current.GoToAsync(nameof(PatientsPage));
        }
    }
}