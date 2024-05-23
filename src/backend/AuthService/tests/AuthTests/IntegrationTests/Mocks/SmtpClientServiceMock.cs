using Application.Abstractions.Email;
using Application.Options;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AuthTests.IntegrationTests.Mocks;

public class SmtpClientServiceMock(IOptions<EmailSenderOptions> options)  : SmtpClient, ISmtpClientService
{
    public override bool IsConnected { get; } = true;

    public new void Connect()
    {
        
    }

    public override Task<string> SendAsync(
        MimeMessage message,
        CancellationToken cancellationToken = default(CancellationToken),
        ITransferProgress progress = null)
    {
        return Task.FromResult("");
    }

    public Task ConnectAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public override void Disconnect(bool quit, CancellationToken cancellationToken = default (CancellationToken))
    {
        
    }
}