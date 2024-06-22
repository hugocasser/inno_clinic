namespace Application.Dtos;

public record DoctorViewDto(
    Guid Id,
    byte[]? Image,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    DateOnly CareerStartDate,
    Guid SpecializationId,
    Guid OfficeId,
    Guid StatusId);