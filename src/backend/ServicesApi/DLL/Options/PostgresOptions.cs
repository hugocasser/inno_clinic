using System.ComponentModel.DataAnnotations;

namespace DLL.Options;

public class PostgresOptions
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Postgres connection string is required")]
    public string ConnectionString { get; set; } = string.Empty;
}