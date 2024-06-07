namespace Application.Abstractions.Services.ExternalServices;

public interface ISpecializationsService
{
    public Task<string> GetSpecializationNameAsync(Guid specializationId, CancellationToken cancellationToken);
}