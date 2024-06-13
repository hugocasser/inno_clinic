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
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public IFormFile? File { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateOnly BirthDate { get; set; }
    Guid SpecializationId { get; set; }
    public DateOnly CareerStarted { get; set; }
    public Guid StatusId { get; set; }
    
    public Guid OfficeId { get; set; }
    public Guid FileId { get; set; }
    public EnumRoles Role { get; set; } = EnumRoles.Doctor;
    public Guid AccountId { get; set; }
    
    private Guid TransactionId { get; set; }
    private bool IsIdSet { get; set; } = false;
    
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
    public FrozenSet<string> GetHandlersKeys()
    {
        return _handlersKeys.ToFrozenSet();
    }

    public Guid GetTransactionId()
    {
        return TransactionId;
    }

    public void SetTransactionId(Guid transactionId)
    {
        if (IsIdSet)
        {
            return;
        }
        
        TransactionId = transactionId;
        IsIdSet = true;
    }

    public static CreateDoctorTransactions MapFromRequest(CreateDoctorsUnitedDto request)
    {
        var transaction = new CreateDoctorTransactions()
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            OfficeId = request.OfficeId,
            File = request.File,
            BirthDate = request.BirthDate,
            SpecializationId = request.SpecializationId,
            CareerStarted = request.CareerStarted,
            StatusId = request.StatusId
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
}