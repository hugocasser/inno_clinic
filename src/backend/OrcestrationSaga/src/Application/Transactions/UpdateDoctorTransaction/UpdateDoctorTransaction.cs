using System.Collections.Frozen;
using Application.Abstractions.Services.Saga;
using Application.Dtos.Requests;
using Application.TransactionComponents.CheckOfficeComponent;
using Application.TransactionComponents.PhotoUpdaterComponent;
using Application.TransactionComponents.UpdateProfileComponent;
using Microsoft.AspNetCore.Http;

namespace Application.Transactions.UpdateDoctorTransaction;

public class UpdateDoctorTransaction
    : ITransactionDto,
        ITransactionWithOfficeId,
        ITransactionWithProfileUpdating,
        ITransactionWithPhotoUpdating
{
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    public Guid OfficeId { get; set; }
    public Guid ProfileId { get; set; }
    public Guid? PhotoId { get; set; }
    public IFormFile? Photo { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public DateOnly CareerStartDate { get; set; }
    public Guid StatusId { get; set; }
    public Guid SpecializationId { get; set; }
    
    
    private readonly HashSet<string> _handlersKeys = 
    [
        CheckOfficeComponentHandler.HandlerKey,
        UpdateProfileComponentHandler.HandlerKey,
        PhotoUpdaterComponentHandler.HandlerKey
    ];

    public void SetPhotoId(Guid? photoId)
    {
        if (photoId is null || photoId.Value == Guid.Empty)
        {
            return;
        }
        
        PhotoId = photoId.Value;
    }

    public static UpdateDoctorTransaction MapFromRequest(UpdateDoctorsUnitedDto request)
    {
        var transaction = new UpdateDoctorTransaction()
        {
            OfficeId = request.OfficeId,
            ProfileId = request.ProfileId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            BirthDate = request.BirthDate,
            CareerStartDate = request.CareerStarted,
            StatusId = request.StatusId,
            SpecializationId = request.SpecializationId
        };
        
        if (request.File is not null)
        {
            transaction.Photo = request.File;
        }
        else
        {
            transaction._handlersKeys.Remove(PhotoUpdaterComponentHandler.HandlerKey);
        }
        
        return transaction;
    }
    
    public FrozenSet<string> GetHandlersKeys()
    {
        return _handlersKeys.ToFrozenSet();
    }
}