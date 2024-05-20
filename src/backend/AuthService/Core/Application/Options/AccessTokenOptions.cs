using System.ComponentModel.DataAnnotations;

namespace Application.Options;

public class AccessTokenOptions
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Issuer is required")]
    public string Issuer { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Audience is required")]
    public string Audience { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Key is required")]
    public string Key { get; set; }
}