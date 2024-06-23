namespace Application.Dtos;

public record ReceptionistViewDto(Guid Id, string FullName, Guid OfficeId, byte[]? Photo);
