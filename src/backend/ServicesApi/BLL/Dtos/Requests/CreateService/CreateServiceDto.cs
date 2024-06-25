namespace BLL.Dtos.Requests;

public record CreateServiceDto(
    string Name,
    Guid SpecializationId,
    Guid CategoryId,
    bool IsActive,
    int Price);