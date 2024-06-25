using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Doctors;

public partial class DoctorCreationPage
{
    public DoctorCreationPage(CreateDoctorViewModel createDoctorViewModel)
    {
        InitializeComponent();
        
        BindingContext = createDoctorViewModel;
    }
}