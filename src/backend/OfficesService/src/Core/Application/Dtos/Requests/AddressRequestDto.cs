using Google.Maps.AddressValidation.V1;
using Google.Type;

namespace Application.Dtos.Requests;

public record AddressRequestDto(string Street, string City, string State, string Country, string PostalCode)
{

    public PostalAddress ToPostalAddress()
    {
        var postalAddress = new PostalAddress
        {
            RegionCode = "",
        };
        return postalAddress;
    }
}