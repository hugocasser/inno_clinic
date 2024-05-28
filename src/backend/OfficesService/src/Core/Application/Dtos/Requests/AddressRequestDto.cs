using Google.Type;

namespace Application.Dtos.Requests;

public record AddressRequestDto(string Street, string City, string State, string Country, string PostalCode)
{

    public PostalAddress ToPostalAddress()
    {
        var postalAddress = new PostalAddress()
        {
            PostalCode = PostalCode,
            RegionCode = Country,
            LanguageCode = "en",
            AddressLines = {Street, City, State}
        };
        return postalAddress;
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {State}, {Country}, {PostalCode}";
    }
}