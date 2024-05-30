using System.Text;
using Application.Abstractions.Services;

namespace Application.Services;

public class PasswordGeneratorService : IPasswordGeneratorService
{
    private const string Letters = "abcdefghijklmnopqrstuvwxyz";
    private const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Numbers = "0123456789";
    private const string Specials = "!?*.-@$%&()";
    private readonly Random _randomizer = new();
    private readonly StringBuilder _passwordBuilder = new();

    public string GenerateRandomPassword()
    {

        
        _passwordBuilder.Append(Letters[_randomizer.Next(Letters.Length)]);
        _passwordBuilder.Append(UppercaseLetters[_randomizer.Next(UppercaseLetters.Length)]);
        _passwordBuilder.Append(Numbers[_randomizer.Next(Numbers.Length)]);
        _passwordBuilder.Append(Specials[_randomizer.Next(Specials.Length)]);
        
        var passwordLength = _randomizer.Next(16, 29);
        
        for (var i = 0; i < passwordLength; i++)
        {
            switch (_randomizer.Next(4))
            {
                case 0:
                    _passwordBuilder.Append(Letters[_randomizer.Next(Letters.Length)]);
                    break;
                case 1:
                    _passwordBuilder.Append(UppercaseLetters[_randomizer.Next(UppercaseLetters.Length)]);
                    break;
                case 2:
                    _passwordBuilder.Append(Numbers[_randomizer.Next(Numbers.Length)]);
                    break;
                case 3:
                    _passwordBuilder.Append(Specials[_randomizer.Next(Specials.Length)]);
                    break;
            }
        }
        
        var password = _passwordBuilder.ToString();
        _passwordBuilder.Clear();
        
        return password;
    }
}