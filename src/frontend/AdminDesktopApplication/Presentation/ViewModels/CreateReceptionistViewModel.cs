using Application.Abstractions.Services;
using Application.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Abstractions.Services;
using Presentation.Abstractions.Services.PipelinedService;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Pages.Profiles.Receptionists;
using Presentation.Resources;

namespace Presentation.ViewModels;

[QueryProperty(nameof(CreateReceptionistModel), "CreateReceptionistModel")]
public partial class CreateReceptionistViewModel : ObservableObject
{
    [ObservableProperty] private CreateReceptionistModel _createReceptionistModel = new();
    [ObservableProperty] private List<OfficeViewDto> _offices = new();
    [ObservableProperty] private List<string> _officesAddresses = new();
    [ObservableProperty] private string _selectedOfficeAddress = null!;

    private readonly IPipelinedProfilesService _profilesService;
    private readonly ICredentialsService _credentialsService;

    public CreateReceptionistViewModel(
        IPipelinedProfilesService profilesService,
        IPipelinedOfficesService officesService,
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
    }

    [RelayCommand]
    private async Task CreateProfileAsync()
    {
        var officeId = Offices.First(x => x.Address == SelectedOfficeAddress).Id;
        var request = await CreateReceptionistModel.MapToRequest();
        request = request with { OfficeId = officeId };
        var requestResult = await _profilesService.CreateReceptionistAsync(request);

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
        await Shell.Current.GoToAsync(nameof(ReceptionistsPage));
    }
}