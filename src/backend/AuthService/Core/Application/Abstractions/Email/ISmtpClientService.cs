using MailKit.Net.Smtp;

namespace Application.Abstractions.Email;

public interface ISmtpClientService : ISmtpClient
{
    public Task ConnectAsync(CancellationToken cancellationToken);
}