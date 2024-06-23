namespace Application.Dtos;

public record PatientViewDto(Guid Id, byte[]? Photo, string FullName, DateOnly Birthday);