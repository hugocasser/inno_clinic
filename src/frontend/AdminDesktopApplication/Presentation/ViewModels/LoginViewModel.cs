using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Abstractions.Services;
using Presentation.Models;
using Presentation.Pages;
using Presentation.Resources;
using Presentation.Validators;

namespace Presentation.ViewModels;

[QueryProperty(nameof(LoginModel), "LoginModel")]
public partial class LoginViewModel(ICredentialsService credentialsService, EmailValidator emailValidator, PasswordValidator passwordValidator) : ObservableObject
{
    [ObservableProperty]
    private string _password  =string.Empty;
    [ObservableProperty]
    private string _email  = string.Empty;
    [ObservableProperty]
    private bool _emailIsValid = true;
    [ObservableProperty]
    private bool _passwordIsValid = true;
    [ObservableProperty]
    private bool _isLoginButtonEnabled = true;
    [ObservableProperty]
    private List<string> _emailValidationErrors = [];
    [ObservableProperty]
    private List<string> _passwordValidationErrors = [];
   
    [RelayCommand]
    private async Task RedirectToRegisterAsync()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage), true);
    }
    
    [RelayCommand]
    private async Task LoginAsync()
    {
        var result = await credentialsService.TryLoginAsync(Email, Password);

        if (result.IsSuccess)
        {
            await Shell.Current.GoToAsync(nameof(MainPage), true);
            return;
        }

        await Shell.Current.DisplayAlert(Language.LoginFailed, Language.IncorrectCredentials + " "+ Email + " " + Password, "Ok");
    }
    
    public void EmailChanged(object? sender, TextChangedEventArgs e)
    {
        Email = e.NewTextValue;
        
        var validationResult = emailValidator.Validate(Email);
        EmailIsValid = !validationResult.IsValid;
        EmailValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
        IsLoginButtonEnabled = !EmailIsValid && !PasswordIsValid;
    }
    
    public void PasswordChanged(object? sender, TextChangedEventArgs e)
    {
        Password = e.NewTextValue;
        
        var validationResult = passwordValidator.Validate(Password);
        PasswordIsValid = !validationResult.IsValid;
        PasswordValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
        IsLoginButtonEnabled = !EmailIsValid && !PasswordIsValid;
    }
}