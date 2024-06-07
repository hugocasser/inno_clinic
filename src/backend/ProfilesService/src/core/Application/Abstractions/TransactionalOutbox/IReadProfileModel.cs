namespace Application.Abstractions.TransactionalOutbox;

public interface IReadProfileModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public bool IsDeleted { get; set; }
};