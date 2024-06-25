namespace Application.Dtos.Patient;

public record PatientViewDto(Guid Id, byte[]? Photo, string FullName, DateOnly Birthday);