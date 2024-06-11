using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Requests;

public record CreateDoctorsUnitedDto
    (string FirstName,
        string LastName,
        string? MiddleName,
        Guid OfficeId,
        string Email,
        string Password,
        DateOnly BirthDate,
        Guid SpecializationId,
        DateOnly CareerStarted,
        Guid StatusId,
        IFormFile? File);