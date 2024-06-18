using Presentation.Abstractions.Services;
using Presentation.Common;
using Presentation.Components.Menu;
using Presentation.ViewModels;

namespace Presentation.Pages;

public partial class MainPage : ContentPage
{

    public MainPage(ICredentialsService credentialsService) : base()
    { 
        InitializeComponent();
        
        if (!credentialsService.CheckLoginAsync().GetAwaiter().GetResult().IsSuccess)
        {
            Shell.Current.GoToAsync(nameof(LoginPage), true)
                .ContinueWith(async _ => await Shell.Current.GoToAsync(nameof(LoginPage), true));
        }
    }
}
