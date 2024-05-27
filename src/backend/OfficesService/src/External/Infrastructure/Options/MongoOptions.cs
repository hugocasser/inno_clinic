using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Options;

public class MongoOptions
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "ConnectionUri is required")]
    public string ConnectionUri { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "DatabaseName is required")]
    public string DatabaseName { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "CollectionsNames is required")]
    public IEnumerable<string> CollectionsNames { get; set; }
}