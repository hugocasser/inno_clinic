using System.Collections.Frozen;
using Application.Abstractions.Services.Saga;
using Application.Dtos.Common;
using Application.Dtos.Requests;
using Application.TransactionComponents.CreateProfileComponent;
using Application.TransactionComponents.RegisterUserComponent;
using Application.TransactionComponents.UploadFileComponent;
using Microsoft.AspNetCore.Http;

namespace Application.Transactions.CreatePatientTransaction;

public class CreatePatientTransaction 
    : ITransactionDto,
      ITransactionWithFileUploading,
      ITransactionWithUserRegistration,
      ITransactionWithProfileCreation
{
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public IFormFile? File { get; set; }
    public DateOnly BirthDate { get; set; }
    
    public EnumRoles Role { get; set; } = EnumRoles.Patient;
    public Guid AccountId { get; set; }
    public Guid FileId { get; set; }
    
    private readonly List<string> _handlersKeys =
    [
        FileUploaderComponentHandler.HandlerKey,
        RegisterUserComponentHandler.HandlerKey,
        CreateProfileComponentHandler.HandlerKey
    ];

    public static CreatePatientTransaction MapFromRequest(CreatePatientsUnitedDto request)
    {
        var transaction = new CreatePatientTransaction
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email,
            Password = request.Password,
            File = request.File,
            BirthDate = request.BirthDate
        };
        
        if (request.File is null)
        {
            transaction._handlersKeys.Remove(FileUploaderComponentHandler.HandlerKey);
        }
        else
        {
            transaction.File = request.File;
        }
        
        return transaction;
    }
    public FrozenSet<string> GetHandlersKeys()
    {
        return _handlersKeys.ToFrozenSet();
    }
    public void SetFileId(Guid fileId)
    {
        FileId = fileId;
    }
    public void SetAccountId(Guid accountId)
    {
        AccountId = accountId;
    }
}