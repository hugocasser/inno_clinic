using Application.Common.Errors;

namespace Application.Abstractions.Results;

public interface IResult
{
    public bool IsSuccess { get; set; }
    public Error Error { get; set; }
    public string GetResultMessage();
    public int? GetStatusCode();
}