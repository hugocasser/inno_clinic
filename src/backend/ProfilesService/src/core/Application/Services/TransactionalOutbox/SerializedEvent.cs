using System.Text;
using Domain.Abstractions.DomainEvents;
using Newtonsoft.Json;

namespace Application.Services.TransactionalOutbox;

public class SerializedEvent
{
    public Guid Id { get; set; }
    public Guid OutboxMessageId { get; set; }
    public OutboxMessage OutboxMessage { get; set; } = null!;
    private readonly string _firstPart;
    private readonly string _secondPart;
    private readonly string _thirdPart;
    private readonly string _fourthPart;
    private readonly string _fifthPart;

    public SerializedEvent(IDomainEvent domainEvent)
    {
        Id = Guid.NewGuid();
        
        var chunks = domainEvent.Serialize().Chunk(5).ToList();
        
        _firstPart = chunks[0].ToString()!;
        _secondPart = chunks[1].ToString()!;
        _thirdPart = chunks[2].ToString()!;
        _fourthPart = chunks[3].ToString()!;
        _fifthPart = chunks[4].ToString()!;
    } 
    
    public void SetMessage(OutboxMessage message)
    {
        OutboxMessageId = message.Id;
        OutboxMessage = message;
    }
    
    public IDomainEvent? GetDomainEvent()
    {
        var stringBuilder = new StringBuilder();
        
        stringBuilder
            .Append(_firstPart)
            .Append(_secondPart)
            .Append(_thirdPart)
            .Append(_fourthPart)
            .Append(_fifthPart);

        return JsonConvert.DeserializeObject<IDomainEvent>(stringBuilder.ToString());
    }
}