using Application.Dtos.Doctor;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class DoctorModel : ObservableObject
{
    [ObservableProperty] private Guid _id;
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string? _middleName;
    [ObservableProperty] private DateOnly _birthDate;
    [ObservableProperty] private DateOnly _careerStartDate;
    [ObservableProperty] private string _specialization = string.Empty;
    [ObservableProperty] private Guid _officeId;
    [ObservableProperty] private string _status = string.Empty;
    [ObservableProperty] private ImageSource? _image;

    public static DoctorModel MapFromResponse(DoctorViewDto response)
    {
        var names = response.FullName.Split(" ");
        var doctor = new DoctorModel
        {
            Id = response.Id,
            FirstName = names[0],
            LastName = names[1],
            MiddleName = names.Length > 2 ? names[2] : null,
            BirthDate = response.BirthDate,
            CareerStartDate = response.CareerStartDate,
            OfficeId = response.OfficeId,
        };

        if (response.Image is not null)
        {
            var stream = new MemoryStream(response.Image);
            var image = ImageSource.FromStream(() => stream);
            if (image is null)
            {
                doctor.Image = "big_empty_profile_photo.png";
                
                return doctor;
            }
            
            doctor.Image = image;
        }
        else
        {
            doctor.Image = "big_empty_profile_photo.png";
        }

        return doctor;
    }
    
    public DoctorModel Copy()
    {
        return new DoctorModel
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            MiddleName = MiddleName,
            BirthDate = BirthDate,
            CareerStartDate = CareerStartDate,
            OfficeId = OfficeId,
            Specialization = Specialization,
            Status = Status,
            Image = Image
        };
    }
    
    public async Task<UpdateDoctorsProfileDto> MapToUpdateRequest(Guid specializationId, Guid statusId, FileResult? image = null)
    {
        if (image is null)
        {
            return new UpdateDoctorsProfileDto
            (Id, 
                null,
                FirstName,
                LastName,
                MiddleName,
                BirthDate,
                CareerStartDate,
                specializationId,
                OfficeId,
                statusId);
        }

        await using var memoryStream = new MemoryStream();

        var imageStream = await image.OpenReadAsync();
        await imageStream.CopyToAsync(memoryStream);
        
        var request = new UpdateDoctorsProfileDto
            (Id, 
                memoryStream.ToArray(),
                FirstName,
                LastName,
                MiddleName,
                BirthDate,
                CareerStartDate,
                specializationId,
                OfficeId,
                statusId);
        
        return request;
    }
}