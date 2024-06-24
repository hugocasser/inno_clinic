using Application.Abstractions;
using Application.Dtos;
using Presentation.Resources;

namespace Presentation.Common;

public static class Utilities
{
    [field: ThreadStatic] public static bool IsUnauthorized { get; set; }
    public static Task CheckResultAndShowAlertWhenFails<T>(IResult result, string message, out T? value) where T : class
    {
        value = null;
        
        if (!result.IsSuccess)
        {
            var error = result.GetResultData<string>();
            
            if (error == TextResponses.Unauthorized)
            {
                IsUnauthorized = true;
                
                return Shell.Current.DisplayAlert(InformMessages.Error, InformMessages.Unathorized, InformMessages.Ok);
            }
            
            return Shell.Current.DisplayAlert(InformMessages.Error, InformMessages.SomethingWentWrong, InformMessages.Ok);
        }
        
        value = result.GetResultData<T>();

        if (result.IsSuccess && result.GetResultData<T>() is not null)
        {
            return Task.CompletedTask;
        }
        
        return Shell.Current.DisplayAlert(InformMessages.Error, InformMessages.SomethingWentWrong, InformMessages.Ok);
    }
}