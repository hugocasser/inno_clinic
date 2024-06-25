using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Patients;

public partial class PatientPage
{
    private readonly PatientViewModel _patientViewModel;
    public PatientPage(PatientViewModel patientViewModel)
    {
        InitializeComponent();

        BindingContext = patientViewModel;
        _patientViewModel = patientViewModel;
    }

    protected override async void OnAppearing()
    {
        await _patientViewModel.InitializeAsync(Guid.NewGuid());
        
        base.OnAppearing();
    }
}