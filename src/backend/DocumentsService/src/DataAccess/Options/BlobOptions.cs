using System.ComponentModel.DataAnnotations;

namespace DataAccess.Options;

public class BlobOptions
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "ContainerName is required")]
    public string ContainerName { get; set; } = string.Empty;
}