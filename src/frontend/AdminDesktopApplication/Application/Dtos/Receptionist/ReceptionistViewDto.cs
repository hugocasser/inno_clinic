namespace Application.Dtos.Receptionist;

public record ReceptionistViewDto(Guid Id, string FullName, Guid OfficeId, byte[]? Photo);
