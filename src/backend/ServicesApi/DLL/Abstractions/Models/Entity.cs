namespace DLL.Abstractions.Models;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}