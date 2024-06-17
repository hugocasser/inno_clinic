using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Receptionists;

public partial class ReceptionistsPage : ContentPage
{
    private readonly ReceptionistListItemViewModel _receptionistListItemViewModel;
    public ReceptionistsPage(ReceptionistListItemViewModel receptionistListItemViewModel)
    {
        InitializeComponent();
        BindingContext = _receptionistListItemViewModel = receptionistListItemViewModel;
        
    }

    private void InputView_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        _receptionistListItemViewModel.SearchCommand.Execute(e.NewTextValue);
    }
}