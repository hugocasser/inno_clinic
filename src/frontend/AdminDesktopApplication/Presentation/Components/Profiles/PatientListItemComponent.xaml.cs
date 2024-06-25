using Presentation.Models;
using Presentation.Pages.Profiles.Patients;

namespace Presentation.Components.Profiles;

public partial class PatientListItemComponent
{
    public PatientListItem PatientListItem
    {
        get => (PatientListItem)GetValue(PatientListItemProperty);
        set => SetValue(PatientListItemProperty, value);
    }

    public static readonly BindableProperty PatientListItemProperty =
        BindableProperty.Create(nameof(PatientListItem), typeof(PatientListItem),
            typeof(PatientListItemComponent), defaultValue: default(PatientListItem));

    public PatientListItemComponent()
    {
        InitializeComponent();
    }

    private async void GoToProfile_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PatientPage), true);
    }
}