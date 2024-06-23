using Application.Abstractions;
using Presentation.Resources;

namespace Presentation.Common;

public static class Utilities
{
    public static Task CheckResultAndShowAlertWhenFails<T>(IResult result, string message, out T? value) where T : class
    {
        value = result.GetResultData<T>();

        if (result.IsSuccess && result.GetResultData<T>() is not null)
        {
            return Task.CompletedTask;
        }

        return Shell.Current.DisplayAlert(InformMessages.Error, message, InformMessages.Ok);
    }
}