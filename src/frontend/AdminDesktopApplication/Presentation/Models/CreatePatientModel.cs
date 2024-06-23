using Application.Dtos.Patient;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class CreatePatientModel : ObservableObject
{
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string? _middleName;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private DateOnly _birthDate = DateOnly.FromDateTime(DateTime.UtcNow);
    
    public CreatePatientDto MapToRequest()
    {
        return new CreatePatientDto
        (
            FirstName,
            LastName,
            MiddleName,
            Email,
            BirthDate
        );
    }
}