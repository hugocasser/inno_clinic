namespace Application.Result;

public class TypedResult <T>
{
    private T Data { get; set; } = default!;
    public bool IsSuccess { get; set; }
    
    public T GetData()
    {
        return Data;
    }
}