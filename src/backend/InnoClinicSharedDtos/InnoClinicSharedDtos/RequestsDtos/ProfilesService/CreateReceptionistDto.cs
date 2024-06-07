namespace InnoClinicSharedDtos.RequestsDtos.ProfilesService;

public record CreateReceptionistDto(
    string FirstName,
    string LastName,
    string MiddleName,
    Guid OfficeId,
    Guid UserId,
    Guid? PhotoId);