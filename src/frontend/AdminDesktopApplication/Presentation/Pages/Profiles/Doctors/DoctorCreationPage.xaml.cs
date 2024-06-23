using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Doctors;

public partial class DoctorCreationPage : ContentPage
{
    public DoctorCreationPage(CreateDoctorViewModel createDoctorViewModel)
    {
        InitializeComponent();
        
        BindingContext = createDoctorViewModel;
    }
}