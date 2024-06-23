using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Patients;

public partial class PatientCreationPage : ContentPage
{
    public PatientCreationPage(CreateDoctorViewModel createDoctorViewModel)
    {
        InitializeComponent();

        BindingContext = createDoctorViewModel;
    }
}