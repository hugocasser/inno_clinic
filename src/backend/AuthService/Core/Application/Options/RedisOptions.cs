using System.ComponentModel.DataAnnotations;

namespace Application.Options;

public class RedisOptions
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Redis connection string is required")]
    public string RefreshTokenConnectionString { get; set; }
}