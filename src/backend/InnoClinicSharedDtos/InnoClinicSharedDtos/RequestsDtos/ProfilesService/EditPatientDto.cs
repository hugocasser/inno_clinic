namespace InnoClinicSharedDtos.RequestsDtos.ProfilesService;

public record EditPatientDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    Guid? PhotoId,
    DateOnly BirthDate);