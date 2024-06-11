using System.Collections.Frozen;
using Application.Abstractions.Services.Saga;
using Application.Dtos.Requests;
using Application.TransactionComponents.PhotoUpdaterComponent;
using Application.TransactionComponents.UpdateProfileComponent;
using Microsoft.AspNetCore.Http;

namespace Application.Transactions.UpdatePatientTransaction;

public class UpdatePatientTransaction 
    : ITransactionDto,
      ITransactionWithProfileUpdating,
      ITransactionWithPhotoUpdating
{
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    public Guid ProfileId { get; set; }
    public Guid? PhotoId { get; private set; }
    public IFormFile? Photo { get; private set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateOnly BirthDate { get; set; }
    
    private readonly HashSet<string> _handlersKeys = 
    [
        UpdateProfileComponentHandler.HandlerKey,
        PhotoUpdaterComponentHandler.HandlerKey
    ];  
    
    public static UpdatePatientTransaction MapFromRequest(UpdatePatientsUnitedDto request)
    {
        var transaction = new UpdatePatientTransaction()
        {
            ProfileId = request.ProfileId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            BirthDate = request.BirthDate,
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

    public void SetPhotoId(Guid? photoId)
    {
        if (photoId is null || photoId.Value == Guid.Empty)
        {
            return;
        }
        
        PhotoId = photoId.Value;
    }
}