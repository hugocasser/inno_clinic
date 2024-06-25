using Application.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Abstractions.Services;
using Presentation.Abstractions.Services.PipelinedService;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Pages.Profiles.Doctors;
using Presentation.Resources;

namespace Presentation.ViewModels;

[QueryProperty(nameof(CreateDoctorModel), "CreateDoctorModel")]
public partial class CreateDoctorViewModel : ObservableObject
{
    [ObservableProperty] private CreateDoctorModel _createDoctorModel = new();
    [ObservableProperty] private List<OfficeViewDto> _offices = [];
    [ObservableProperty] private List<string> _officesAddresses = [];
    [ObservableProperty] private string _selectedOfficeAddress = null!;
    [ObservableProperty] private string _selectedSpecialization = string.Empty;
    [ObservableProperty] private List<string> _specializationsNames = [];
    [ObservableProperty] private List<SpecializationViewDto> _specializations = [];
    [ObservableProperty] private string _selectedStatus = string.Empty;
    [ObservableProperty] private List<string> _statusesNames = [];
    [ObservableProperty] private List<StatusViewDto> _statuses = [];

    private readonly IPipelinedProfilesService _profilesService;
    private readonly ICredentialsService _credentialsService;

    public CreateDoctorViewModel(
        IPipelinedProfilesService profilesService,
        IPipelinedOfficesService officesService,
        IPipelinedSpecializationsService specializationsService,
        IPipelinedStatusesService statusesService,
        ICredentialsService credentialsService)
    {
        _profilesService = profilesService;
        _credentialsService = credentialsService;

        officesService.GetAllAsync()
            .ContinueWith(_ =>
            {
                var result = officesService.GetAllAsync().Result;
                var offices = result.GetResultData<List<OfficeViewDto>>();

                if (!result.IsSuccess || offices is null)
                {
                    Shell.Current.GoToAsync(nameof(MainPage)).Wait();

                    return;
                }

                Offices = offices;
                OfficesAddresses = offices.Select(x => x.Address).ToList();
                SelectedOfficeAddress = OfficesAddresses.First();
            });
        
        specializationsService.GetAllSpecializationsAsync()
            .ContinueWith(_ =>
            {
                var result = specializationsService.GetAllSpecializationsAsync().Result;
                var specializations = result.GetResultData<List<SpecializationViewDto>>();

                if (!result.IsSuccess || specializations is null)
                {
                    Shell.Current.GoToAsync(nameof(MainPage)).Wait();

                    return;
                }

                Specializations = specializations;
                SpecializationsNames = specializations.Select(x => x.Name).ToList();
                SelectedSpecialization = Specializations.First().Name;
            });
        
        statusesService.GetAllStatusesAsync()
            .ContinueWith(_ =>
            {
                var result = statusesService.GetAllStatusesAsync().Result;
                var statuses = result.GetResultData<List<StatusViewDto>>();
                
                if (!result.IsSuccess || statuses is null)
                {
                    Shell.Current.GoToAsync(nameof(MainPage)).Wait();

                    return;
                }
                
                Statuses = statuses;
                StatusesNames = statuses.Select(x => x.Name).ToList();
                SelectedStatus = Statuses.First().Name;
            });
    }

    [RelayCommand]
    private async Task CreateProfileAsync()
    {
        var officeId = Offices.First(x => x.Address == SelectedOfficeAddress).Id;
        var specializationId = Specializations.First(x => x.Name == SelectedSpecialization).Id;
        var statusId = Statuses.First(x => x.Name == SelectedStatus).Id;
        var request = await CreateDoctorModel.MapToRequest();
        request = request with { OfficeId = officeId, SpecializationId = specializationId, StatusId = statusId };
        
        var requestResult = await _profilesService.CreateDoctorProfileAsync(request);

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
                    await _credentialsService.LogoutAsync();
                    
                    await Shell.Current.DisplayAlert(InformMessages.Error, InformMessages.Unathorized, InformMessages.Ok);
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                    
                    break;
                }
            }
        }

        await Shell.Current.DisplayAlert(InformMessages.Success, InformMessages.ProfileCreated, InformMessages.Ok);
        await Shell.Current.GoToAsync(nameof(DoctorsPage));
    }
}