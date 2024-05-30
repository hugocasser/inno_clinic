using Domain.Abstractions;
using Domain.Abstractions.DomainEvents;

namespace Application.Abstractions.TransactionalOutbox;

public interface IOutboxMessage
{
    public void Create<T>(T entity) where T : Profile;
    public IDomainEvent GetDomainEvent();
}