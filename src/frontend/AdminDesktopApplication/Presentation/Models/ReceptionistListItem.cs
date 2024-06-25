using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class ReceptionistListItem : ObservableObject
{
    [ObservableProperty]
    private Guid _id;
    [ObservableProperty]
    private string _fullName = default!;
    [ObservableProperty] 
    private ImageSource? _photo;
}