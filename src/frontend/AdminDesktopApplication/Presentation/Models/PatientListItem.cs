using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class PatientListItem : ObservableObject
{
    [ObservableProperty]
    private Guid _id;
    [ObservableProperty]
    private string _fullName = default!;
    [ObservableProperty] 
    private DateOnly _dateOfBirth;
    [ObservableProperty] 
    private ImageSource? _photo;
}