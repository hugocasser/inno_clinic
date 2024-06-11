using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Requests;

public record CreatePatientsUnitedDto(string Email, string Password,
    IFormFile? File, string FirstName, string LastName, string? MiddleName, DateOnly BirthDate){}