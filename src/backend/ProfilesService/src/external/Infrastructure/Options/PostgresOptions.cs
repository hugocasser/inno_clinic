using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Options;

public class PostgresOptions
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "ConnectionString is required")]
    public string ConnectionString { get; set; } = null!;
}