using Application.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class PatientModel : ObservableObject
{
    [ObservableProperty] private Guid _id;
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string? _middleName;
    [ObservableProperty] private DateOnly _birthDate;
    [ObservableProperty] private ImageSource? _image;

    public static PatientModel MapFromResponse(PatientViewDto patient)
    {
        var names = patient.FullName.Split(" ");
        
        var model = new PatientModel
        {
            Id = patient.Id,
            FirstName = names[0],
            LastName = names[1],
            MiddleName = names.Length > 2 ? names[2] : null,
            BirthDate = patient.Birthday
        };
        
        if (patient.Photo is not null)
        {
            var stream = new MemoryStream(patient.Photo);
            var image = ImageSource.FromStream(() => stream);
            if (image is null)
            {
                model.Image = "big_empty_profile_photo.png";
                
                return model;
            }
            
            model.Image = image;
        }
        else
        {
            model.Image = "big_empty_profile_photo.png";
        }
        
        return model;
    }
}