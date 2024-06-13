namespace Presentation.Components.Auth;

public partial class PasswordInput : ContentView
{
    public static readonly BindableProperty PasswordProperty = BindableProperty.Create(nameof(Password), typeof(string), typeof(PasswordInput));
    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }
    public PasswordInput()
    {
        Password = string.Empty;
        InitializeComponent();
    }

    public bool IsPasswordValid
    {
        get => (bool)GetValue(IsPasswordValidProperty);
        set => SetValue(IsPasswordValidProperty, !value);
    }
    
    public static readonly BindableProperty IsPasswordValidProperty = BindableProperty.Create(nameof(IsPasswordValid), typeof(bool), typeof(PasswordInput));

    public string ValidationError { get; set; }= "password is not valid";
    
    public event EventHandler? PasswordChanged ;

    public static readonly BindableProperty EmailChangedCommandProperty = BindableProperty.Create(nameof(PasswordChanged), typeof(EventHandler), typeof(EmailInput));
    
    private void InputView_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        Password = e.NewTextValue;

        PasswordChanged?.Invoke(this, e);
    }
}