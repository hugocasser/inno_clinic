using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Requests;

public record CreateReceptionistsUnitedDto
    (string Email,
        string Password,
        string FirstName,
        string LastName,
        string? MiddleName,
        Guid OfficeId,
        IFormFile? Photo);