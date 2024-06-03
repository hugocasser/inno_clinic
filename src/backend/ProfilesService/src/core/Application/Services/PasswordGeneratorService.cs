using ValueStringBuilder = Application.Common.ValueStringBuilder;
using Application.Abstractions.Services;

namespace Application.Services;

public class PasswordGeneratorService : IPasswordGeneratorService
{
    private const string Letters = "abcdefghijklmnopqrstuvwxyz";
    private const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Numbers = "0123456789";
    private const string Specials = "!?*.-@$%&()";
    private readonly Random _random = Random.Shared;

    public string GenerateRandomPassword()
    {
        var passwordBuilder = new ValueStringBuilder(stackalloc char[32]);
        
        passwordBuilder.Append(Letters[_random.Next(Letters.Length)]);
        passwordBuilder.Append(UppercaseLetters[_random.Next(UppercaseLetters.Length)]);
        passwordBuilder.Append(Numbers[_random.Next(Numbers.Length)]);
        passwordBuilder.Append(Specials[_random.Next(Specials.Length)]);
        
        var passwordLength = _random.Next(16, 29);
        
        for (var i = 0; i < passwordLength; i++)
        {
            switch (_random.Next(4))
            {
                case 0:
                    passwordBuilder.Append(Letters[_random.Next(Letters.Length)]);
                    break;
                case 1:
                    passwordBuilder.Append(UppercaseLetters[_random.Next(UppercaseLetters.Length)]);
                    break;
                case 2:
                    passwordBuilder.Append(Numbers[_random.Next(Numbers.Length)]);
                    break;
                case 3:
                    passwordBuilder.Append(Specials[_random.Next(Specials.Length)]);
                    break;
            }
        }
        
        var password = passwordBuilder.ToString();

        return password;
    }
}