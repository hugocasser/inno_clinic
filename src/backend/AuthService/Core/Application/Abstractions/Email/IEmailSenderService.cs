using Application.Dtos;

namespace Application.Abstractions.Email;

public interface IEmailSenderService
{
    public Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken);
}