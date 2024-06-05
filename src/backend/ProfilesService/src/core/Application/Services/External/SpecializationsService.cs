using Application.Abstractions.Services.ExternalServices;

namespace Application.Services.External;

public class SpecializationsService : ISpecializationsService
{
    //TODO: implement specialization service
    // now it's just mock while i don't implement services communication
    public Task<string> GetSpecializationNameAsync(Guid specializationId, CancellationToken cancellationToken)
    {
        return Task.FromResult("Specialization name");
    }
}