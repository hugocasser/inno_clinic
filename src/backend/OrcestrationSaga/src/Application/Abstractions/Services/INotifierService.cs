namespace Application.Abstractions.Services;

public interface INotifierService
{
    public void AddMessageToNotify(string message);
    public Task NotifyAsync(CancellationToken cancellationToken = default);
}