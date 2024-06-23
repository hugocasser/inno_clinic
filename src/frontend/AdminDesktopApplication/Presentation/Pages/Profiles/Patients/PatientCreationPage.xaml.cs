using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Patients;

public partial class PatientCreationPage : ContentPage
{
    public PatientCreationPage(CreatePatientViewModel createDoctorViewModel)
    {
        InitializeComponent();

        BindingContext = createDoctorViewModel;
    }
}