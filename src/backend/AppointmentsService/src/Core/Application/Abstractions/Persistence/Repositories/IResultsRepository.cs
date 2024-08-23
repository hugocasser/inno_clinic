namespace Application.Abstractions.Persistence.Repositories;

public interface IResultsRepository
{
    public Task<bool> IsExistAsync(Guid requestId, CancellationToken cancellationToken);
    public Task<int> UpdateAsync(Guid requestId, string requestComplaints, string requestConclusion, string requestRecommendation, CancellationToken cancellationToken);
    public Task<int> AddAsync(object resultToCreate, CancellationToken cancellationToken);
}