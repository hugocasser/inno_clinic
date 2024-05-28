using Domain.Models;
using MediatR;

namespace Domain.Abstractions.Events;

public interface IDomainEvent<out T> where T : Entity
{
    public T GetEntity();
    public string GetEventType();
};