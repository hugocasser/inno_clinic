using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Requests;

public record UpdatePatientsUnitedDto(Guid ProfileId,
    IFormFile? File,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate);