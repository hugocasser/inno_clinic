using Application.Abstractions.Services.Saga;
using Application.Services;
using Application.Services.Notifications;
using Application.TransactionComponents.CheckOfficeComponent;
using Application.TransactionComponents.CreateProfileComponent;
using Application.TransactionComponents.DeleteAccountComponent;
using Application.TransactionComponents.DeletePhotoComponent;
using Application.TransactionComponents.DeleteProfileComponent;
using Application.TransactionComponents.PhotoUpdaterComponent;
using Application.TransactionComponents.RegisterUserComponent;
using Application.TransactionComponents.UpdateProfileComponent;
using Application.TransactionComponents.UploadFileComponent;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddTransactionsHandling();
        
        return services;
    }
    
    private static IServiceCollection AddComponentHandlers(this IServiceCollection services)
    {
        services
            .AddKeyedScoped<ITransactionComponentHandler, PhotoUpdaterComponentHandler>(PhotoUpdaterComponentHandler
                .HandlerKey)
            .AddKeyedScoped<ITransactionComponentHandler, UpdateProfileComponentHandler>(UpdateProfileComponentHandler
                .HandlerKey)
            .AddKeyedScoped<ITransactionComponentHandler, CheckOfficeComponentHandler>(CheckOfficeComponentHandler
                .HandlerKey)
            .AddKeyedScoped<ITransactionComponentHandler, CreateProfileComponentHandler>(CreateProfileComponentHandler
                .HandlerKey)
            .AddKeyedScoped<ITransactionComponentHandler, FileUploaderComponentHandler>(FileUploaderComponentHandler
                .HandlerKey)
            .AddKeyedScoped<ITransactionComponentHandler, RegisterUserComponentHandler>(RegisterUserComponentHandler
                .HandlerKey)
            .AddKeyedScoped<ITransactionComponentHandler, DeletePhotoComponentHandler>(DeletePhotoComponentHandler
                .HandlerKey)
            .AddKeyedScoped<ITransactionComponentHandler, DeleteProfileComponentHandler>(DeleteProfileComponentHandler
                .HandlerKey)
            .AddKeyedScoped<ITransactionComponentHandler, DeleteAccountComponentHandler>(DeleteAccountComponentHandler
                .HandlerKey);
        
        return services;
    }

    private static IServiceCollection AddTransactionsHandling(this IServiceCollection services)
    {
        services.AddComponentHandlers();
        services.AddSingleton<ITransactionsHandlerService, TransactionsHandlerService>();
        services.AddSingleton<ITransactionsNotifierService, TransactionsNotifierService>();
        
        return services;
    }
}