using Presentation.Abstractions.Services;

namespace Presentation.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(ICredentialsService credentialsService)
    { 
        InitializeComponent();
        
        if (!credentialsService.CheckLoginAsync().GetAwaiter().GetResult())
        {
            Shell.Current.GoToAsync(nameof(LoginPage), true)
                .ContinueWith(async _ => await Shell.Current.GoToAsync(nameof(MainPage), true));
        }
    }
}
