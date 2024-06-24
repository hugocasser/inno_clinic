using Application.Dtos.Receptionist;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Models;

public partial class CreateReceptionistModel : ObservableObject
{
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string? _middleName;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _confirmPassword = string.Empty;
    [ObservableProperty] private FileResult? _photo;
    [ObservableProperty] private ImageSource _photoSource = "big_empty_profile_photo.png";
    [ObservableProperty] private Guid _officeId = Guid.Empty;

    public async Task<CreateReceptionistDto> MapToRequest()
    {
        if (Photo is null)
        {
            return new CreateReceptionistDto
            (
                FirstName,
                LastName,
                MiddleName,
                Email,
                Password,
                OfficeId,
                null
            );
        }

        await using var photoStream = await Photo.OpenReadAsync();
        await using var memoryStream = new MemoryStream();
        await photoStream.CopyToAsync(memoryStream);
        
        return new CreateReceptionistDto
        (
            FirstName,
            LastName,
            MiddleName,
            Email,
            Password,
            OfficeId,
            memoryStream.ToArray()
        );
    }
}