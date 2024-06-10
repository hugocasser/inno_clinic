using System.Collections.Frozen;
using Application.Abstractions.Services.Saga;
using Application.Dtos.Common;
using Application.Dtos.Requests;
using Application.TransactionComponents.CheckOfficeComponent;
using Application.TransactionComponents.CreateProfileComponent;
using Application.TransactionComponents.RegisterUserComponent;
using Application.TransactionComponents.UploadFileComponent;
using Microsoft.AspNetCore.Http;

namespace Application.Transactions.CreateDoctorTransaction;

public class CreateDoctorTransactions :
    ITransactionDto,
    ITransactionWithOfficeId,
    ITransactionWithFileUploading,
    ITransactionWithUserRegistration,
    ITransactionWithProfileCreation
{
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public EnumRoles Role { get; set; }
    public Guid AccountId { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public Guid OfficeId { get; set; }
    public IFormFile Photo { get; set; } = null!;
    public Guid FileId { get; set; }
    private readonly List<string> _handlersKeys =
    [
        CheckOfficeComponentHandler.HandlerKey,
        FileUploaderComponentHandler.HandlerKey,
        RegisterUserComponentHandler.HandlerKey,
        CreateProfileComponentHandler.HandlerKey
    ];
    public void SetFileId(Guid fileId)
    {
        FileId = fileId;
    }
    public void SetAccountId(Guid accountId)
    {
        AccountId = accountId;
    }
    public FrozenSet<string> GetOrderedHandlersKeys()
    {
        return _handlersKeys.ToFrozenSet();
    }

    public static CreateDoctorTransactions MapFromRequest(CreateDoctorsProfileDto request)
    {
        var transaction = new CreateDoctorTransactions();
        
        if (request.Photo is null)
        {
            transaction._handlersKeys.Remove(FileUploaderComponentHandler.HandlerKey);
        }
        else
        {
            transaction.Photo = request.Photo;
        }
        
        return transaction;
    }
}