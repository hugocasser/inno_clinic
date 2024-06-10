using System.ComponentModel.DataAnnotations;

namespace DataAccess.Options;

public class AzureOptions
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "ConnectionUrl is required")]
    public string ConnectionUrl { get; set; } = string.Empty;
}