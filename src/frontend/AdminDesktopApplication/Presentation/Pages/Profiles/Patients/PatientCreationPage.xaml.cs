using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Patients;

public partial class PatientCreationPage
{
    public PatientCreationPage(CreatePatientViewModel createDoctorViewModel)
    {
        InitializeComponent();

        BindingContext = createDoctorViewModel;
    }
}