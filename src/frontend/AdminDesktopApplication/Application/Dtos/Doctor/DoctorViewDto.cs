namespace Application.Dtos.Doctor;

public record DoctorViewDto(
    Guid Id,
    byte[]? Image,
    string FullName,
    DateOnly BirthDate,
    DateOnly CareerStartDate,
    Guid SpecializationId,
    Guid OfficeId,
    Guid StatusId);