namespace Application.Dtos.Validation.PhoneValidation;

public record PhoneValidatorResponse
    (string Phone, bool Valid, Format Format, Country Country, string Location, string Type, string Carrier);