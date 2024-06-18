using Presentation.ViewModels;

namespace Presentation.Components.Menu;

public partial class MenuComponent : ContentView
{

    public MenuComponent()
    {
        InitializeComponent();
        BindingContext = new MenuViewModel();
    }
}