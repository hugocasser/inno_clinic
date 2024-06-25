using System.ComponentModel.DataAnnotations;
using BLL.Abstractions;
using DLL.Models;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace BLL.Dtos.Requests.CreateSpecialization;

public record CreateSpecializationDto(string Name, bool IsActive) : IRequestDto
{ 
    public  Specialization MapToModel()
    {
        return new Specialization
        {
            Id = Guid.NewGuid(),
            Name = Name,
            IsActive = IsActive
        };
    }

    public ValidationResult Validate()
    {
        return new CreateSpecializationDtoValidator().Validate(this);
    }
}