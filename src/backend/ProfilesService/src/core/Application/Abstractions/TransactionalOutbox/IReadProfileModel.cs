namespace Application.Abstractions.TransactionalOutbox;

public interface IReadProfileModel<T>
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
};