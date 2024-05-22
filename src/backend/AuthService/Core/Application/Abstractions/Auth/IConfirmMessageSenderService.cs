using Domain.Models;

namespace Application.Abstractions.Auth;

public interface IConfirmMessageSenderService
{
    public Task SendEmailConfirmMessageAsync(User user, CancellationToken cancellationToken);
}