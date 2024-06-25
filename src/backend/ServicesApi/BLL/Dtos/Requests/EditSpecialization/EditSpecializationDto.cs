using System.ComponentModel.DataAnnotations;
using BLL.Abstractions;
using DLL.Models;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace BLL.Dtos.Requests.EditSpecialization;

public record EditSpecializationDto(Guid Id, bool IsActive, string Name) : IRequestDto
{
    public Specialization MapToModel()
    {
        return new Specialization
        {
            Id = Id,
            Name = Name,
            IsActive = IsActive
        };
    }

    public ValidationResult Validate()
    {
        return new EditSpecializationDtoValidator().Validate(this);
    }
};