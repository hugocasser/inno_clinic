using System.Text;
using Application.Abstractions.Results;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common;

public static class Utilities
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
        
        return ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(errors));
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
    
    public static IServiceCollection AddGenericOptions<T>(this IServiceCollection services, string configSectionPath) where T : class
    {
        services.AddOptions<T>()
            .BindConfiguration(configSectionPath)
            .ValidateOnStart()
            .ValidateDataAnnotations();
        
        return services;
    }
}