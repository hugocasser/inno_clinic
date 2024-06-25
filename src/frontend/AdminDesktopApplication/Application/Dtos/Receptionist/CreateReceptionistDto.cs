namespace Application.Dtos.Receptionist;

public record CreateReceptionistDto(
    string FirstName,
    string LastName,
    string? MiddleName,
    string Email,
    string Password,
    Guid OfficeId,
    byte[]? Photo);