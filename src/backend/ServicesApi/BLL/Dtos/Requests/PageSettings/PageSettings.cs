using System.ComponentModel.DataAnnotations;
using BLL.Abstractions;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace BLL.Dtos.Requests.PageSettings;

public record PageSettings(int PageNumber, int PageSize) : IRequestDto
{
    public ValidationResult Validate()
    {
        return new PageSettingValidator().Validate(this);
    }
}