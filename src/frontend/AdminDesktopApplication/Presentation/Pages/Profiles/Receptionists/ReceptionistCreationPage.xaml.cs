using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Receptionists;

public partial class ReceptionistCreationPage
{
    public ReceptionistCreationPage(CreateReceptionistViewModel createReceptionistViewModel)
    {
        InitializeComponent();

        BindingContext = createReceptionistViewModel;
    }
}