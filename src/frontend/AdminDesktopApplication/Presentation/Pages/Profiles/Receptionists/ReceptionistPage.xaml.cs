using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Receptionists;

public partial class ReceptionistPage
{
    private readonly ReceptionistViewModel _receptionistViewModel;
    public ReceptionistPage(ReceptionistViewModel receptionistViewModel)
    {
        InitializeComponent();
        
        _receptionistViewModel = receptionistViewModel;
        
        BindingContext = _receptionistViewModel;
    }
    
    protected override async void OnAppearing()
    {
        await _receptionistViewModel.InitializeAsync(Guid.NewGuid());
        
        base.OnAppearing();
    } 
}