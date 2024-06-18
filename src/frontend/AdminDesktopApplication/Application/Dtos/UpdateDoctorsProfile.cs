using Microsoft.AspNetCore.Http;

namespace Application.Dtos;

public record UpdateDoctorsProfile
    (
        Guid Id,
        IFormFile? Image,
        string FirstName,
        string LastName,
        string? MiddleName,
        DateOnly BirthDate,
        DateOnly CareerStartDate,
        Guid SpecializationId,
        Guid OfficeId,
        Guid StatusId);