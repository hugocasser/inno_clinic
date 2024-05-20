using System.ComponentModel.DataAnnotations;

namespace Application.Options;

public class EmailSenderOptions
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Sender is required")]
    [EmailAddress(ErrorMessage = "Email address is not valid")]
    public string Sender { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Smtp server url is required")]
    [Url(ErrorMessage = "Smtp server url is not valid")]
    public string SmtpServer { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Port is required")]
    public int Port { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "User name is required")]
    public string UserName { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
    [Length(8, 32, ErrorMessage = "Password must be between 8 and 32 characters")]
    public string Password { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm url is required")]
    [Url(ErrorMessage = "Confirm url is not valid")]
    public string ConfirmUrl { get; set; }
    
    public bool UseSsl { get; set; } = false;
}