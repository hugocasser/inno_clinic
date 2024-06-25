using DLL.Abstractions.Models;

namespace DLL.Models;

public class Service : Entity
{
    public int Price { get; set; } = -1;
    public Guid SpecializationId { get; set; }
    public bool IsActive { get; set; } = false;
    public Guid CategoryId { get; set; }
}