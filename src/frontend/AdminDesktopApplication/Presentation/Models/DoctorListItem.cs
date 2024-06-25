using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class DoctorListItem : ObservableObject
{
    [ObservableProperty]
    private Guid _id;
    [ObservableProperty]
    private string _fullName = default!;
    [ObservableProperty] 
    private DateOnly _dateOfBirth;
    [ObservableProperty] 
    private ImageSource? _photo;
    [ObservableProperty]
    private string _specialization = default!;
    [ObservableProperty]
    private string _status = default!;
    
    public DoctorListItem Item => this;
}