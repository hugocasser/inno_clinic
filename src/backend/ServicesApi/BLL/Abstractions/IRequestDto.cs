using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace BLL.Abstractions;

public interface IRequestDto
{
    public ValidationResult Validate(); 
}