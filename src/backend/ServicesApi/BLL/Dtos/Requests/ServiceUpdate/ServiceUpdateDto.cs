using BLL.Abstractions;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace BLL.Dtos.Requests.ServiceUpdate;

public record ServiceUpdateDto
    (Guid Id, string Name, bool IsActive, int Price, Guid SpecializationId, Guid CategoryId) : IRequestDto
{
    public ValidationResult Validate()
    {
        return new ServiceUpdateDtoValidator().Validate(this);
    }
}