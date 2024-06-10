using System.Collections.Frozen;
using Application.Abstractions.Services.Saga;
using Application.Dtos.Common;
using Application.TransactionComponents.CheckOfficeComponent;
using Application.TransactionComponents.CreateProfileComponent;
using Application.TransactionComponents.RegisterUserComponent;
using Application.TransactionComponents.UploadFileComponent;
using Microsoft.AspNetCore.Http;

namespace Application.Transactions.CreateReceptionistTransaction;

public class CreateReceptionistTransaction :
    ITransactionDto,
    ITransactionWithOfficeId,
    ITransactionWithFileUploading,
    ITransactionWithUserRegistration,
    ITransactionWithProfileCreation
{
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    public FrozenSet<string> GetOrderedHandlersKeys()
    {
        return _handlersKeys.ToFrozenSet();
    }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public Guid OfficeId { get; set; }
    public IFormFile Photo { get; set; }
    public Guid FileId { get; set; }
    public void SetFileId(Guid fileId)
    {
        FileId = fileId;
    }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public EnumRoles Role { get; set; }
    public Guid AccountId { get; set; }
    public void SetAccountId(Guid accountId)
    {
        AccountId = accountId;
    }

    private readonly List<string> _handlersKeys = 
        [
            CheckOfficeComponentHandler.HandlerKey,
            FileUploaderComponentHandler.HandlerKey,
            RegisterUserComponentHandler.HandlerKey,
            CreateProfileComponentHandler.HandlerKey,
        ];
}