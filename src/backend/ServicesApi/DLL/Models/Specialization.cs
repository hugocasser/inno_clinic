using DLL.Abstractions.Models;

namespace DLL.Models;

public class Specialization : Entity
{
    public bool IsActive { get; set; } = false;
}