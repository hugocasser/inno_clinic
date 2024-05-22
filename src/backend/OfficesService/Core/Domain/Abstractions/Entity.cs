using Domain.Abstractions.Events;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Abstractions;

public abstract class Entity
{
    [BsonElement("id")] public Guid Id { get; protected set; } = Guid.Empty;
    
    private readonly IList<IDomainEvent> _events = new List<IDomainEvent>();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }

    public IEnumerable<IDomainEvent> GetDomainEvents()
    {
        return _events.ToList().AsReadOnly();
    }

    public void ClearDomainEvents()
    {
        _events.Clear();
    }

    public void Delete(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }
}