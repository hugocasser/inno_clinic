namespace Application.Abstractions.Services;

public interface IDataSeederService
{
    public Task SeedRecordsAsync(CancellationToken cancellationToken = default);
}