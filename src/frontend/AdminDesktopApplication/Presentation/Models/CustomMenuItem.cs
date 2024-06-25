using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class CustomMenuItem : ObservableObject
{
    [ObservableProperty]
    private string _name = null!;
    [ObservableProperty]
    private ICommand _command = null!;
}