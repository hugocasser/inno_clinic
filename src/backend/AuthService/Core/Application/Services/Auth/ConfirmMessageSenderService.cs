using System.Text;
using Application.Abstractions.Auth;
using Application.Abstractions.Email;
using Application.Dtos;
using Application.Options;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Application.Services.Auth;

public class ConfirmMessageSenderService(IEmailSenderService emailSenderService, IOptions<EmailSenderOptions> emailSenderOptions, UserManager<User> userManager) : IConfirmMessageSenderService
{
    public async Task SendEmailConfirmMessageAsync(User user, CancellationToken cancellationToken)
    {
        var token = await GenerateConfirmationToken(user);

        var confirmUrl = emailSenderOptions.Value.ConfirmUrl + $"{user.Id}/{token}";
        var emailBody = GenerateEmailBody(user, confirmUrl);
        var message = GenerateEmailMessage(user, "Email confirmation", emailBody);
        await emailSenderService.SendEmailAsync(message, cancellationToken);
    }

    private static string GenerateEmailBody(User user, string confirmUrl)
    {
        return $"<h1>Hi, dear client! Thank you for registering :3</h1></br>" +
            $"Please confirm your email address <a href={System.Text.Encodings.Web.HtmlEncoder.Default.Encode(confirmUrl)}>Confirm</a>";
    }

    private static EmailMessage GenerateEmailMessage(User user, string subject, string emailBody)
    {
        return new EmailMessage(user.Email, subject, emailBody, "User");
    }

    private async Task<string> GenerateConfirmationToken(User user)
    {
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
        var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

        return codeEncoded;
    }
}