using System.Text;
using Application.Abstractions.Results;
using Application.Common.Errors;
using Application.Results;
using Microsoft.AspNetCore.Identity;

namespace Application.Common;

public class Utilities
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    public static IResult AggregateIdentityResult(IdentityResult result)
    {
        if (result.Succeeded)
        {
            return ResultWithoutContent.Success();
        }
        
        var errors = result.Errors.Aggregate(string.Empty,
            (current, error) => current + (error.Description + "\n"));
        
        return ResultWithoutContent.Failure(Error.BadRequest().WithMessage(errors));
    }

    public static string GenerateRandomString(int length)
    {
        var random = new Random();
        var result = new StringBuilder(length);
        
        for (var i = 0; i < length; i++)
        {
            result.Append(Chars[random.Next(Chars.Length)]);
        }
        
        return result.ToString();
    }
}