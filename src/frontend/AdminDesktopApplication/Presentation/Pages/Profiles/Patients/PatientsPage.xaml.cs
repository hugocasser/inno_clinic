
using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Patients;

public partial class PatientsPage : ContentPage
{
    private readonly PatientListItemViewModel _patientListItemViewModel;

    public PatientsPage(PatientListItemViewModel patientListItemViewModel)
    {
        InitializeComponent();

        BindingContext = _patientListItemViewModel = patientListItemViewModel;
    }

    private void InputView_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        _patientListItemViewModel.SearchCommand.Execute(e.NewTextValue);
    }
}