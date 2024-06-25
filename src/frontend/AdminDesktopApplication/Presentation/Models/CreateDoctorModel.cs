using Application.Dtos.Doctor;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class CreateDoctorModel : ObservableObject
{
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string? _middleName;
    [ObservableProperty] private Guid _specializationId = Guid.Empty;
    [ObservableProperty] private Guid _statusId = Guid.Empty;
    [ObservableProperty] private DateOnly _birthDate = DateOnly.FromDateTime(DateTime.Now);
    [ObservableProperty] private DateOnly _careerStartDate = DateOnly.FromDateTime(DateTime.Now);
    [ObservableProperty] private FileResult? _photo;
    [ObservableProperty] private ImageSource _photoSource = "big_empty_profile_photo.png";
    [ObservableProperty] private Guid _officeId = Guid.Empty;
    
    public async Task<CreateDoctorsProfileDto> MapToRequest()
    {
        if (Photo is null)
        {
            return new CreateDoctorsProfileDto
            (
                null,
                FirstName,
                LastName,
                MiddleName,
                BirthDate,
                CareerStartDate,
                SpecializationId,
                OfficeId,
                StatusId
            );
        }

        await using var photoStream = await Photo.OpenReadAsync();
        await using var memoryStream = new MemoryStream();
        await photoStream.CopyToAsync(memoryStream);
        
        return new CreateDoctorsProfileDto
        (
            memoryStream.ToArray(),
            FirstName,
            LastName,
            MiddleName,
            BirthDate,
            CareerStartDate,
            SpecializationId,
            OfficeId,
            StatusId
        );
    }
}