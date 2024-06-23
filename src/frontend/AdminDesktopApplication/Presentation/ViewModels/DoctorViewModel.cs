using Application.Abstractions.Services;
using Application.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics.Platform;
using Presentation.Abstractions.Services;
using Presentation.Common;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Pages.Profiles.Doctors;
using Presentation.Resources;

namespace Presentation.ViewModels;

[QueryProperty(nameof(DoctorModel), "DoctorModel")]
public partial class DoctorViewModel(
    IStatusesMapperService statusesMapperService,
    ISpecializationsMapperService specializationsMapperService,
    IProfilesService profilesService)
    : ObservableObject
{
    [ObservableProperty] private DoctorModel _doctor = null!;
    [ObservableProperty] private DoctorModel _doctorEditModel = null!;
    [ObservableProperty] private bool _isMiddleNameVisible;
    [ObservableProperty] private bool _isInEditMode;
    [ObservableProperty] private bool _isInViewMode = true;
    [ObservableProperty] private List<string> _specializationsToDisplay = null!;
    [ObservableProperty] private List<string> _statusesToDisplay = null!;
    [ObservableProperty] private string _fullName = null!;
    [ObservableProperty] private int _selectedSpecializationIndex;
    [ObservableProperty] private int _selectedStatusIndex;
    private FileResult? UploadedImage { get; set; }

    private List<KeyValuePair<Guid, string>> _specializationsList = [];
    private List<KeyValuePair<Guid, string>> _statusesList = [];

    public async Task Initialize(Guid id)
    {
        var requestResult = await profilesService.GetDoctorProfileAsync(id);

        await Utilities.CheckResultAndShowAlertWhenFails<DoctorViewDto>(requestResult, InformMessages.ProfileNotFound, out var doctor);

        if (doctor is null)
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
            return;
        }

        var model = DoctorModel.MapFromResponse(doctor);

        var statusResult = await statusesMapperService.GetStatusNameByIdAsync(doctor.StatusId);
        await Utilities.CheckResultAndShowAlertWhenFails<string>(statusResult, InformMessages.StatusNotFound, out var statusName);

        if (statusName is null)
        {
            await Shell.Current.GoToAsync(nameof(MainPage));

            return;
        }

        var specializationResult =
            await specializationsMapperService.GetSpecializationNameByIdAsync(doctor.SpecializationId);

        await Utilities.CheckResultAndShowAlertWhenFails<string>(specializationResult,
            InformMessages.SpecializationNotFound, out var specializationName);

        if (specializationName is null)
        {
            await Shell.Current.GoToAsync(nameof(MainPage));

            return;
        }

        model.Status = statusName;
        model.Specialization = specializationName;

        var specializations = await specializationsMapperService.GetAllSpecializationsAsync();

        await Utilities.CheckResultAndShowAlertWhenFails<Dictionary<Guid, string>>(specializations,
            InformMessages.SpecializationsNotFound, out var specializationsDictionary);

        if (specializationsDictionary is null)
        {
            await Shell.Current.GoToAsync(nameof(MainPage));

            return;
        }

        _specializationsList = specializationsDictionary.ToList();
        SpecializationsToDisplay = _specializationsList.Select(x => x.Value).ToList();

        var statuses = await statusesMapperService.GetAllStatusesAsync();

        await Utilities.CheckResultAndShowAlertWhenFails<Dictionary<Guid, string>>(statuses, InformMessages.StatusesNotFound,
            out var statusesDictionary);

        if (!statuses.IsSuccess || statusesDictionary is null)
        {
            await Shell.Current.GoToAsync(nameof(MainPage));

            return;
        }

        _statusesList = statusesDictionary.ToList();
        StatusesToDisplay = _statusesList.Select(x => x.Value).ToList();

        SelectedSpecializationIndex = _specializationsList.FindIndex(x => x.Key == doctor.SpecializationId);
        SelectedStatusIndex = _statusesList.FindIndex(x => x.Key == doctor.StatusId);

        Doctor = model;

        DoctorEditModel = Doctor.Copy();
        FullName = NavigationResources.Doctor + $": {Doctor.FirstName} {Doctor.LastName}";
        
        if (!string.IsNullOrEmpty(Doctor.MiddleName))
        {
            IsMiddleNameVisible = true;
        }
    }

    [RelayCommand]
    private async Task UploadPhotoAsync()
    {
        if (IsInViewMode)
        {
            return;
        }

        try
        {
            var pickResult = await FilePicker.Default.PickAsync();

            if (pickResult is null)
            {
                return;
            }
    
            if (pickResult.FullPath.ToLower().EndsWith(nameof(FilesTypes.png))
                || pickResult.FullPath.ToLower().EndsWith(nameof(FilesTypes.jpg))
                || pickResult.FullPath.ToLower().EndsWith(nameof(FilesTypes.jpeg))
                || pickResult.FullPath.ToLower().EndsWith(nameof(FilesTypes.webp)))
            {
                await using var readStream = await pickResult.OpenReadAsync();

                var image = PlatformImage
                    .FromStream(readStream)
                    .Resize(180, 180,
                        ResizeMode.Fit, true).AsStream();

                DoctorEditModel.Image = ImageSource.FromStream(() => image);

                UploadedImage = pickResult;
            }
        }
        catch
        {
            await Shell.Current.DisplayAlert(InformMessages.Error, InformMessages.Error, InformMessages.Ok);
        }
    }

    [RelayCommand]
    private void StartEdit()
    {
        IsInViewMode = false;
        IsInEditMode = true;
        IsMiddleNameVisible = true;
    }

    [RelayCommand]
    private async Task CancelEdit()
    {
        var result = await Shell.Current
            .DisplayAlert(InformMessages.AreYouSure,
                InformMessages.Cancel_,
                InformMessages.Continue,
                InformMessages.Back);

        if (!result)
        {
            return;
        }

        if (string.IsNullOrEmpty(Doctor.MiddleName))
        {
            IsMiddleNameVisible = false;
        }

        DoctorEditModel = Doctor.Copy();
        IsInViewMode = true;
        IsInEditMode = false;
    }

    [RelayCommand]
    private async Task Delete()
    {
        var result = await Shell.Current
            .DisplayAlert(InformMessages.AreYouSure,
                InformMessages.WontCanRollbackProfile,
                InformMessages.Continue,
                InformMessages.Back);

        if (!result)
        {
            return;
        }

        var requestResult = await profilesService.DeleteProfileAsync(Doctor.Id);

        if (!requestResult.IsSuccess)
        {
            await Shell.Current.DisplayAlert(InformMessages.Error, InformMessages.Error, InformMessages.Ok);
            return;
        }

        await Shell.Current.GoToAsync(nameof(DoctorsPage));
    }

    [RelayCommand]
    private async Task SaveEdit()
    {
        var result = await Shell.Current
            .DisplayAlert(InformMessages.AreYouSure,
                InformMessages.Save_,
                InformMessages.Continue,
                InformMessages.Back);

        if (!result)
        {
            return;
        }

        var specializationId = _specializationsList.First(spec =>
            spec.Value == SpecializationsToDisplay[SelectedSpecializationIndex]).Key;

        var statusId = _statusesList.First(status =>
            status.Value == StatusesToDisplay[SelectedStatusIndex]).Key;

        var updateRequest = await DoctorEditModel
            .MapToUpdateRequest(specializationId, statusId, UploadedImage);

        var updateResult = await profilesService
            .UpdateDoctorProfileAsync(updateRequest);

        if (!updateResult.IsSuccess)
        {
            await Shell.Current
                .DisplayAlert(InformMessages.Error,
                    InformMessages.IncorrectCredentials,
                    InformMessages.Ok);

            return;
        }

        IsInViewMode = true;
        IsInEditMode = false;

        if (string.IsNullOrEmpty(Doctor.MiddleName))
        {
            IsMiddleNameVisible = false;
        }

        Doctor = DoctorEditModel.Copy();
    }
}