using Domain.Abstractions;
using Domain.DomainEvents;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Office: Entity
{
    [BsonElement("address")]
    public string? Address { get; private set; } = string.Empty;
    [BsonElement("registry_phone_number")]
    public string? RegistryPhoneNumber { get; private set; } = string.Empty;
    [BsonElement("is_active")]
    public bool IsActive { get; private set; } = false;
    [BsonElement("photo_id")]
    public Guid PhotoId { get; private set; } = Guid.Empty;
    
    public static Office CreateOffice(string address, string registryPhoneNumber, bool isActive, Guid photoId = default)
    {
        var office = new Office
        {
            Id = Guid.NewGuid(),
            Address = address,
            RegistryPhoneNumber = registryPhoneNumber,
            IsActive = isActive,
            PhotoId = photoId
        };

        office.RaiseDomainEvent(new EntityCreatedEvent<Office>(office));
        
        return office;
    }
    
    public void UpdateOffice(bool isActive, Guid photoId = default,
        string? registryPhoneNumber = default, string? address = default)
    {
        if (registryPhoneNumber is null && address is null
            && isActive == IsActive && photoId == PhotoId)
        {
            return;
        }
        
        if (registryPhoneNumber is not null)
        {
            RegistryPhoneNumber = registryPhoneNumber;
        }
        
        if (address is not null)
        {
            Address = address;
        }
        
        if (photoId != default)
        {
            PhotoId = photoId;
        }
        
        if (isActive != IsActive)
        {
            IsActive = isActive;
        }
        
        Address = address;
        RegistryPhoneNumber = registryPhoneNumber;
        IsActive = isActive;
        PhotoId = photoId;
        
        RaiseDomainEvent(new EntityUpdatedEvent<Office>(this));
    }
    
    public void DeleteOffice()
    {
        RaiseDomainEvent(new EntityDeletedEvent<Office>(this));
    }
}