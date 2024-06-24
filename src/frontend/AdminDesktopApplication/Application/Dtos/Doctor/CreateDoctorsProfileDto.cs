namespace Application.Dtos.Doctor;

public record CreateDoctorsProfileDto(
    byte[]? Image,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    DateOnly CareerStartDate,
    Guid SpecializationId,
    Guid OfficeId,
    Guid StatusId);