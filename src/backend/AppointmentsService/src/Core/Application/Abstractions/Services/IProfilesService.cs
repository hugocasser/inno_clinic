namespace Application.Abstractions.Services;

public interface IProfilesService
{
    public Task<bool> IsPatientExist(Guid userId, CancellationToken cancellationToken = default);
    public Task<bool> IsDoctorInSpecializationExist(Guid doctorId, Guid specializationId, CancellationToken cancellationToken = default);
    public Task<bool> IsDoctorInOffice(Guid doctorId, Guid officeId, CancellationToken cancellationToken = default);
}