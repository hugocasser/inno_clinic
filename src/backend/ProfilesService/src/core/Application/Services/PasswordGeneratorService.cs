using ValueStringBuilder = Application.Common.ValueStringBuilder;
using Application.Abstractions.Services;

namespace Application.Services;

public class PasswordGeneratorService : IPasswordGeneratorService
{
    private const string Letters = "abcdefghijklmnopqrstuvwxyz";
    private const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Numbers = "0123456789";
    private const string Specials = "!?*.-@$%&()";

    public string GenerateRandomPassword()
    {
        var passwordBuilder = new ValueStringBuilder(stackalloc char[32]);
        
        passwordBuilder.Append(Letters[Random.Shared.Next(Letters.Length)]);
        passwordBuilder.Append(UppercaseLetters[Random.Shared.Next(UppercaseLetters.Length)]);
        passwordBuilder.Append(Numbers[Random.Shared.Next(Numbers.Length)]);
        passwordBuilder.Append(Specials[Random.Shared.Next(Specials.Length)]);
        
        var passwordLength = Random.Shared.Next(16, 29);
        
        for (var i = 0; i < passwordLength; i++)
        {
            switch (Random.Shared.Next(4))
            {
                case 0:
                    passwordBuilder.Append(Letters[Random.Shared.Next(Letters.Length)]);
                    break;
                case 1:
                    passwordBuilder.Append(UppercaseLetters[Random.Shared.Next(UppercaseLetters.Length)]);
                    break;
                case 2:
                    passwordBuilder.Append(Numbers[Random.Shared.Next(Numbers.Length)]);
                    break;
                case 3:
                    passwordBuilder.Append(Specials[Random.Shared.Next(Specials.Length)]);
                    break;
            }
        }
        
        var password = passwordBuilder.ToString();

        return password;
    }
}