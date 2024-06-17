using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Doctors;

public partial class DoctorsPage : ContentPage
{
    private readonly DoctorListItemViewModel _doctorListItemViewModel;
    public DoctorsPage(DoctorListItemViewModel doctorListItemViewModel)
    {
        InitializeComponent();
        
        BindingContext = _doctorListItemViewModel = doctorListItemViewModel;
    }

    private void InputView_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        _doctorListItemViewModel.SearchCommand.Execute(e.NewTextValue);
    }
}