namespace InnoClinicSharedDtos.RequestsDtos.ProfilesService;

public record CreateDoctorDto(
    Guid? PhotoId,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    Guid UserId,
    Guid SpecializationId,
    Guid OfficeId,
    DateOnly CareerStarted,
    Guid StatusId);