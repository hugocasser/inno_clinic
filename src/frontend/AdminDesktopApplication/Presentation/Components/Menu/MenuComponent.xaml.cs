using System.ComponentModel;
using Presentation.ViewModels;

namespace Presentation.Components.Menu;

public partial class MenuComponent
{
    private readonly MenuViewModel _menuViewModel = new();
    private readonly Animation _menuAnimation;
    public MenuComponent()
    {
        InitializeComponent();
        BindingContext = _menuViewModel;
        _menuAnimation = new Animation(
            value => VerticalStackLayoutComponent.Opacity = value , 0, 1, Easing.Linear);
        
        _menuViewModel.PropertyChanged += MenuViewModelOnPropertyChanged;
    }

    private void MenuViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MenuViewModel.IsMenuVisible))
        {
            if (_menuViewModel.IsMenuVisible)
            {
                _menuAnimation.Commit(this, nameof(MenuComponent), 16, 500);
            }
            else
            {
                _menuAnimation.Commit(this, nameof(MenuComponent), 16, 500);
            }
        }
        
        Shell.Current.BackgroundColor = _menuViewModel.IsMenuVisible ? Colors.PaleVioletRed : Colors.Beige;
    }
}