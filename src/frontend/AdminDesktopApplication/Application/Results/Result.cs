using Application.Abstractions;

namespace Application.Results;

public class Result : IResult
{
    public bool IsSuccess { get; set; } = false;
}