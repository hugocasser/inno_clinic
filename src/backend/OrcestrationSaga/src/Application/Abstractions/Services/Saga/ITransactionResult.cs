namespace Application.Abstractions.Services.Saga;

public interface ITransactionResult : IResult
{
    public bool NeedRollback { get; set; }
    public bool IsFinished { get; set; }
}