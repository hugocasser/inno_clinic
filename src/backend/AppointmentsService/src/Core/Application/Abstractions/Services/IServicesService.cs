namespace Application.Abstractions.Services;

public interface IServicesService
{
    public Task<bool> IsServiceExist(Guid serviceId, CancellationToken cancellationToken = default);
    public Task<bool> IsSpecializationExist(Guid specializationId);
}