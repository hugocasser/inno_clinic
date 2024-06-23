using Application.Dtos;
using Application.Dtos.Receptionist;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class ReceptionistModel : ObservableObject
{
    [ObservableProperty] private Guid _id;
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string? _middleName;
    [ObservableProperty] private ImageSource? _image;

    public static ReceptionistModel MapFromResponse(ReceptionistViewDto receptionist)
    {
        var names = receptionist.FullName.Split(" ");
        
        var model = new ReceptionistModel
        {
            Id = receptionist.Id,
            FirstName = names[0],
            LastName = names[1],
            MiddleName = names.Length > 2 ? names[2] : null,
        };
        
        if (receptionist.Photo is not null)
        {
            var stream = new MemoryStream(receptionist.Photo);
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