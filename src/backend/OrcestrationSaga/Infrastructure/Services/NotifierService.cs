using Application.Abstractions.Services;

namespace Infrastructure.Services;

public class NotifierService : INotifierService
{
    public void AddMessageToNotify(string message)
    {
        return;
    }

    public Task NotifyAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}