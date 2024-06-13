namespace InnoClinicSharedDtos.RequestsDtos.ProfilesService;

public record EditDoctorDto
    (Guid DoctorId,
    Guid? PhotoId,
    string FirstName, 
    string LastName, 
    string? MiddleName, 
    DateOnly BirthDate, 
    Guid SpecializationId, 
    Guid OfficeId, 
    DateOnly CareerStarted, 
    Guid StatusId);