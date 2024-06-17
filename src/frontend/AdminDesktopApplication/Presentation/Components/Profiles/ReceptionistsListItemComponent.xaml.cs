using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Models;
using Presentation.Pages.Profiles.Receptionists;

namespace Presentation.Components.Profiles;

public partial class ReceptionistsListItemComponent : ContentView
{
    public ReceptionistsListItemComponent()
    {
        InitializeComponent();
    }
    
    public ReceptionistListItem ReceptionistListItem
    {
        get => (ReceptionistListItem)GetValue(ReceptionistListItemProperty);
        set => SetValue(ReceptionistListItemProperty, value);
    }
    
    public static readonly BindableProperty ReceptionistListItemProperty =
        BindableProperty.Create(nameof(ReceptionistListItem), typeof(ReceptionistListItem),
            typeof(ReceptionistsListItemComponent), defaultValue: default(ReceptionistListItem));
    
    private async void GoToProfile_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ReceptionistPage), true);
    }
}