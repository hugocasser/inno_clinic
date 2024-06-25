using DLL.Models;

namespace BLL.Dtos.Requests.CreateSpecialization;

public class CreateSpecializationDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
    public  Specialization MapToModel()
    {
        return new Specialization
        {
            Id = Guid.NewGuid(),
            Name = Name,
            IsActive = IsActive
        };
    }
}