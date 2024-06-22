using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Doctors;

public partial class DoctorPage
{
    private readonly DoctorViewModel _doctorViewModel;
    public DoctorPage(DoctorViewModel doctorViewModel)
    {
        InitializeComponent();
        _doctorViewModel = doctorViewModel;
        BindingContext = _doctorViewModel;
    }

    protected override async void OnAppearing()
    {
        await _doctorViewModel.Initialize(Guid.NewGuid());
        
        base.OnAppearing();
    }
}