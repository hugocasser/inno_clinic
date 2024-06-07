using System.ComponentModel.DataAnnotations;
using System.Text;
using Domain.Abstractions.DomainEvents;
using Newtonsoft.Json;

namespace Application.Services.TransactionalOutbox;

public class SerializedEvent
{
    public Guid Id { get; init; }
    public Guid OutboxMessageId { get; set; }
    public OutboxMessage OutboxMessage { get; set; } = null!;
    [MaxLength(2000)]
    private string FirstPart { get; init; } = string.Empty; 
    [MaxLength(2000)]
    private string SecondPart { get; init; } = string.Empty;
    [MaxLength(2000)]
    private string ThirdPart { get; init; } = string.Empty;
    [MaxLength(2000)]
    private string FourthPart { get; init; } = string.Empty;
    [MaxLength(2000)]
    private string FifthPart { get; init; } = string.Empty;

    public static SerializedEvent Create(IDomainEvent domainEvent)
    {
        var chunks = domainEvent.Serialize().Chunk(5).ToList();
        
        return new SerializedEvent()
        {
            Id = Guid.NewGuid(),
            FirstPart = chunks[0].ToString()!,
            SecondPart = chunks[1].ToString()!,
            ThirdPart = chunks[2].ToString()!,
            FourthPart = chunks[3].ToString()!,
            FifthPart = chunks[4].ToString()!
        };
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
            .Append(FirstPart)
            .Append(SecondPart)
            .Append(ThirdPart)
            .Append(FourthPart)
            .Append(FifthPart);

        return JsonConvert.DeserializeObject<IDomainEvent>(stringBuilder.ToString());
    }
}