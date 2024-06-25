namespace BLL.Dtos.Requests.ServiceUpdate;

public record ServiceUpdateDto(Guid Id, string Name, bool IsActive, int Price, Guid SpecializationId, Guid CategoryId);