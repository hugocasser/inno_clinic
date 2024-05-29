namespace Domain.Abstractions;

public abstract class Profile
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public bool IsDeleted { get; set; }
    public Guid PhotoId { get; set; }
}