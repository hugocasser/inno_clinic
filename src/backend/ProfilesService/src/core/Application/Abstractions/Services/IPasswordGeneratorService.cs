namespace Application.Abstractions.Services;

public interface IPasswordGeneratorService
{
    public string GenerateRandomPassword();
}