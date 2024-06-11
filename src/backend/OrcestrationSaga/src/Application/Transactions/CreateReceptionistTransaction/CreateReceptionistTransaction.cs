using System.Collections.Frozen;
using Application.Abstractions.Services.Saga;
using Application.Dtos.Common;
using Application.Dtos.Requests;
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
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public Guid OfficeId { get; set; }
    public IFormFile? File { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public Guid FileId { get; set; }
    public EnumRoles Role { get; set; } = EnumRoles.Receptionist;
    public Guid AccountId { get; set; }
    
    private readonly List<string> _handlersKeys = 
    [
        CheckOfficeComponentHandler.HandlerKey,
        FileUploaderComponentHandler.HandlerKey,
        RegisterUserComponentHandler.HandlerKey,
        CreateProfileComponentHandler.HandlerKey,
    ];
    
    public void SetFileId(Guid fileId)
    {
        FileId = fileId;
    }
    public void SetAccountId(Guid accountId)
    {
        AccountId = accountId;
    }
    public FrozenSet<string> GetHandlersKeys()
    {
        return _handlersKeys.ToFrozenSet();
    }
    public static CreateReceptionistTransaction MapFromRequest(CreateReceptionistsUnitedDto request)
    {
        var transaction = new CreateReceptionistTransaction
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            OfficeId = request.OfficeId,
            File = request.Photo
        };
        
        if (request.Photo is null)
        {
            transaction._handlersKeys.Remove(FileUploaderComponentHandler.HandlerKey);
        }
        else
        {
            transaction.File = request.Photo;
        }
        
        return transaction;
    }
}