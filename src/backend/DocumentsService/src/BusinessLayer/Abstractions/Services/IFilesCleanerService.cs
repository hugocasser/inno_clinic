namespace BusinessLayer.Abstractions.Services;

public interface IFilesCleanerService
{
    public Task CleanAsync(CancellationToken cancellationToken);
}