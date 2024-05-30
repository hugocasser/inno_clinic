using System.Collections.ObjectModel;
using Domain.Abstractions.DomainEvents;

namespace Domain.Abstractions;

public abstract class Profile
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public bool IsDeleted { get; set; }
    public Guid PhotoId { get; set; }

    public ReadOnlyCollection<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.AsReadOnly();
    }

    public void RaiseEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);  
    }
}