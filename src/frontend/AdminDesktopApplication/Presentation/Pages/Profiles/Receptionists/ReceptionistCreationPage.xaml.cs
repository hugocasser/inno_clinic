using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Receptionists;

public partial class ReceptionistCreationPage : ContentPage
{
    public ReceptionistCreationPage(CreateReceptionistViewModel createReceptionistViewModel)
    {
        InitializeComponent();

        BindingContext = createReceptionistViewModel;
    }
}