using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Patients;

public partial class PatientViewAsDoctor
{
    private readonly PatientListItemViewModel _patientListItemViewModel;
    public PatientViewAsDoctor(PatientListItemViewModel patientListItemViewModel)
    {
        InitializeComponent();

        BindingContext = _patientListItemViewModel = patientListItemViewModel;
    }
}