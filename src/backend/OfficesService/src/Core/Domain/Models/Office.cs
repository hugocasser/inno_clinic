using Domain.Abstractions;
using Domain.Abstractions.Events;
using Domain.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Office: Entity
{
    [BsonElement("address")]
    [BsonRepresentation(BsonType.String)]
    public string? Address { get;  set; } = string.Empty;
    [BsonElement("registry_phone_number")]
    [BsonRepresentation(BsonType.String)]
    public string? RegistryPhoneNumber { get; set; } = string.Empty;
    [BsonElement("is_active")]
    [BsonRepresentation(BsonType.Boolean)]
    public bool IsActive { get;  set; }
    [BsonElement("photo_id")]
    [BsonRepresentation(BsonType.String)]
    public Guid PhotoId { get;  set; } = Guid.Empty;
    
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

        office.RaiseDomainEvent(new GenericDomainEvent(EventType.Created, office));
        
        return office;
    }
    
    public void ChangeOfficePhoto(Guid photoId = default)
    {
        if (photoId == PhotoId)
        {
            return;
        }
        
        PhotoId = photoId;
        RaiseDomainEvent(new GenericDomainEvent(EventType.Updated, this));
    }
    public void UpdateOffice(Guid photoId = default,
        string? registryPhoneNumber = default, string? address = default)
    {
        if (registryPhoneNumber is null && address is null && photoId == PhotoId)
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
        
        Address = address;
        RegistryPhoneNumber = registryPhoneNumber;
        PhotoId = photoId;
        
        RaiseDomainEvent(new GenericDomainEvent(EventType.Updated, this));
    }
    
    public void ChangeOfficeStatus(bool isActive)
    {
        if (isActive == IsActive)
        {
            return;
        }
        
        IsActive = isActive;
        RaiseDomainEvent(new GenericDomainEvent(EventType.Updated, this));
    }
    
    public void DeleteOffice()
    {
        RaiseDomainEvent(new GenericDomainEvent(EventType.Deleted, this));
    }
}