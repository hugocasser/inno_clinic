namespace Presentation.Components.Menu;

public partial class MenuComponent : ContentView
{

    public bool IsMenuVisible
    {
        get => (bool)GetValue(IsMenuVisibleProperty);
        set => SetValue(IsMenuVisibleProperty, value);
    }
    
    public static readonly BindableProperty IsMenuVisibleProperty = BindableProperty.Create(nameof(IsMenuVisible), typeof(bool), typeof(MenuComponent), true);
    public MenuComponent()
    {
        InitializeComponent();
        IsMenuVisible = true;
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        IsMenuVisible = !IsMenuVisible;
    }
}