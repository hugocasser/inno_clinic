using Presentation.ViewModels;

namespace Presentation.Pages;

public partial class LoginPage
{
    private readonly LoginViewModel _loginViewModel;
    public LoginPage(LoginViewModel loginViewModel)
    {
        BindingContext = loginViewModel;
        _loginViewModel = loginViewModel;
        
        InitializeComponent();
        
        if (Shell.Current.Window != null)
        {
            Shell.Current.Window.MaximumHeight = 800;
            Shell.Current.Window.MaximumWidth = 500;
        }
    }

    private void InputEmailView_OnEmailChanged(object? sender, EventArgs eventArgs)
    {
        _loginViewModel.EmailChanged(sender, (eventArgs as TextChangedEventArgs)!);
    }
    
    private void InputPasswordView_OnPasswordChanged(object? sender, EventArgs eventArgs)
    {
        _loginViewModel.PasswordChanged(sender, (eventArgs as TextChangedEventArgs)!);
    }
}