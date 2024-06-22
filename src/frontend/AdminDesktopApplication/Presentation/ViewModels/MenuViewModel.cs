using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Common;
using Presentation.Models;

namespace Presentation.ViewModels;

[QueryProperty(nameof(CustomMenuItem), nameof(IsMenuVisible))]
public partial class MenuViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isMenuVisible;

    [ObservableProperty] 
    private ObservableCollection<CustomMenuItem> _menuItems = MenuConfiguratorService.Configure("");
    
    [RelayCommand]
    private void ToggleMenu()
    {
        IsMenuVisible = !IsMenuVisible;
    }
}