using Domain.Abstractions.Events;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Abstractions;

public abstract class Entity
{
    [BsonElement("id")] public Guid Id { get; protected set; } = Guid.Empty;
    
    private readonly IList<IDomainEvent<Entity>> _events = new List<IDomainEvent<Entity>>();

    protected void RaiseDomainEvent(IDomainEvent<Entity> domainEvent)
    {
        _events.Add(domainEvent);
    }

    public IEnumerable<IDomainEvent<Entity>> GetDomainEvents()
    {
        return _events.ToList().AsReadOnly();
    }

    public void ClearDomainEvents()
    {
        _events.Clear();
    }

    public void Delete(IDomainEvent<Entity> domainEvent)
    {
        _events.Add(domainEvent);
    }
}