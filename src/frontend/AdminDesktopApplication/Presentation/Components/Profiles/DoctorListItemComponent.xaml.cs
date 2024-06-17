using Presentation.Models;
using Presentation.Pages.Profiles.Doctors;

namespace Presentation.Components.Profiles;

public partial class DoctorListItemComponent : ContentView
{
    public DoctorListItem DoctorListItem
    {
        get => (DoctorListItem)GetValue(DoctorListItemProperty);
        set => SetValue(DoctorListItemProperty, value);
    }
    
    public static readonly BindableProperty DoctorListItemProperty =
        BindableProperty.Create(nameof(DoctorListItem), typeof(DoctorListItem),
            typeof(DoctorListItemComponent), defaultValue: default(DoctorListItem));
    
    public DoctorListItemComponent()
    {
        InitializeComponent();
    }
    
    private async void GoToProfile_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DoctorSelfView), true);
    }
}