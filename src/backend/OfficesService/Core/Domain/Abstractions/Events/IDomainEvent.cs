using MediatR;

namespace Domain.Abstractions.Events;

public interface IDomainEvent : INotification
{
    public string GetEventType();
};