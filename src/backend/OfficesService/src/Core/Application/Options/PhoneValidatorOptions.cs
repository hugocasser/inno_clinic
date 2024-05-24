using System.ComponentModel.DataAnnotations;

namespace Application.Options;

public class PhoneValidatorOptions
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Api key is required")]
    public string ApiKey { get; set; } = default!;
}