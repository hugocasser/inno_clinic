namespace InnoClinicSharedDtos.RequestsDtos.ProfilesService;

public record CreatePatientDto(
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    Guid UserId,
    Guid? PhotoId);