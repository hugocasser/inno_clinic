using System.Windows.Input;
using Presentation.ViewModels;

namespace Presentation.Components.Auth;

public partial class EmailInput : ContentView
{
    public EmailInput()
    {
        InitializeComponent();
    }
    public string Email
    {
        get => (string)GetValue(EmailProperty);
        set => SetValue(EmailProperty, value);
    }
    
    public static readonly BindableProperty EmailProperty = BindableProperty.Create(nameof(Email), typeof(string), typeof(EmailInput));
    
    public bool IsEmailValid
    {
        get => (bool)GetValue(IsEmailValidProperty);
        set => SetValue(IsEmailValidProperty, value);
    }
    
    public static readonly BindableProperty IsEmailValidProperty = BindableProperty.Create(nameof(IsEmailValid), typeof(bool), typeof(EmailInput));
    
    public ICommand EmailChangedCommand
    {
        get => (ICommand)GetValue(EmailChangedCommandProperty);
        set => SetValue(EmailChangedCommandProperty, value);
    }

    public event EventHandler? EmailChanged ;

    public static readonly BindableProperty EmailChangedCommandProperty = BindableProperty.Create(nameof(EmailChanged), typeof(EventHandler), typeof(EmailInput)); 
    
    private void InputView_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        Email = e.NewTextValue;

        EmailChanged?.Invoke(this, e);
    }
}