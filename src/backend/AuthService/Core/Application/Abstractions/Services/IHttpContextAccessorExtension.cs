namespace Application.Abstractions.Services;

public interface IHttpContextAccessorExtension
{
    public string? GetUserId();
}