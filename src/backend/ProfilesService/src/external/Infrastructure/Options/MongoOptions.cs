using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Options;

public class MongoOptions
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "ConnectionString is required")]
    public string ConnectionString { get; set; } = null!;
    [Required(AllowEmptyStrings = false, ErrorMessage = "DatabaseName is required")]
    public string DatabaseName { get; set; } = null!;
}