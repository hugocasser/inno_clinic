using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class DoctorModel : ObservableObject
{
    [ObservableProperty]
    private Guid _id;
    [ObservableProperty]
    private string FirstName;
    [ObservableProperty]
    private string LastName;
    [ObservableProperty]
    private string? MiddleName;
    [ObservableProperty]
    private DateOnly BirthDate;
    [ObservableProperty]
    private DateOnly CareerStartDate;
    [ObservableProperty]
    private string _specialization;
    [ObservableProperty]
    private Guid OfficeId;
    [ObservableProperty]
    private string _status;
    [ObservableProperty]
    private ImageSource? Image;
}