using System.Collections.Frozen;
using Application.Abstractions.Services.Saga;
using Application.TransactionComponents.DeleteAccountComponent;
using Application.TransactionComponents.DeletePhotoComponent;
using Application.TransactionComponents.DeleteProfileComponent;

namespace Application.Transactions.DeleteDoctorTransaction;

public class DeleteDoctorTransaction 
    : ITransactionDto,
      ITransactionWithProfileDeleting,
      ITransactionWithPhotoDeleting,
      ITransactionWithAccountDeleting
{
    public Guid? PhotoId { get; set; }
    public Guid AccountId { get; set; }
    public Guid ProfileId { get; set; }
    
    private Guid TransactionId { get; set; }
    private bool IsIdSet { get; set; } = false;
    private readonly List<string> _handlersKeys = 
    [
        DeleteProfileComponentHandler.HandlerKey,
        DeletePhotoComponentHandler.HandlerKey,
        DeleteAccountComponentHandler.HandlerKey
    ];
    
    public FrozenSet<string> GetHandlersKeys()
    {
        return _handlersKeys.ToFrozenSet();
    }
    
    public void SetAccountId(Guid accountId)
    {
        AccountId = accountId;
    }

    public void SetPhotoId(Guid? photoId)
    {
        if (photoId == null)
        {
            PhotoId = null;
            _handlersKeys.Remove(DeletePhotoComponentHandler.HandlerKey);
            
            return;
        }
        
        PhotoId = photoId;
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
}