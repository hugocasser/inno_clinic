using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class ReceptionistModel : ObservableObject
{
    [ObservableProperty] private Guid _id;
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string? _middleName;
    [ObservableProperty] private ImageSource? _image;
}