using Application.Resources;
using FluentValidation;

namespace Application.Common;

public static class ValidationExtensions
{
    public static void ResultDescriptionString<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        ruleBuilder
            .MinimumLength(50)
            .WithMessage(ValidationMessages.MinLengh50)
            .MaximumLength(2000)
            .WithMessage(ValidationMessages.MaxLenght2000)
            .NotEmptyWithMessage();
    }

    private static void NotEmptyWithMessage<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeEmpty);
    }
    
    public static void NotEmptyWithMessage<T>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeEmpty);
    }
    
    public static void NotEmptyWithMessage<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeEmpty);
    }
}