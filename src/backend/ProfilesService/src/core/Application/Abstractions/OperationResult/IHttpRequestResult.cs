namespace Application.Abstractions.OperationResult;

public interface IHttpRequestResult : IResult
{
    public int GetStatusCode();
    public object GetContent();
}