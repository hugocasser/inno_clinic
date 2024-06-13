using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Requests;

public record UpdateDoctorsUnitedDto
    (Guid ProfileId,
        IFormFile? File,
        string FirstName,
        string LastName,
        string? MiddleName,
        DateOnly BirthDate,
        Guid SpecializationId,
        DateOnly CareerStarted,
        Guid StatusId,
        Guid OfficeId);