namespace Application.Dtos.Patient;

public record CreatePatientDto(
    string FirstName,
    string LastName,
    string? MiddleName,
    string Email,
    DateOnly Birthday);