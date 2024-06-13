namespace Presentation.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        
        if (Shell.Current.Window != null)
        {
            Shell.Current.Window.MaximumHeight = 800;
            Shell.Current.Window.MaximumWidth = 500;
        }
    }

    private async void LoginRedirectButton_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage), true);
    }
}