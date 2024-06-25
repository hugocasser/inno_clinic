using System.ComponentModel.DataAnnotations;
using BLL.Abstractions;
using DLL.Models;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace BLL.Dtos.Requests.CreateService;

public record CreateServiceDto(
    string Name,
    Guid SpecializationId,
    Guid CategoryId,
    bool IsActive,
    int Price) : IRequestDto
{
    public Service MapToModel()
    {
        return new Service
        {
            Id = Guid.NewGuid(),
            Name = Name,
            SpecializationId = SpecializationId,
            CategoryId = CategoryId,
            IsActive = IsActive,
            Price = Price
        };
    }

    public ValidationResult Validate()
    {
        return new CreateServiceDtoValidator().Validate(this);
    }
};