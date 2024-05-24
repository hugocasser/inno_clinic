using Domain.Models;

namespace Application.Services.Specification.Offices;

public class OfficesByNumber : BaseSpecification<Office>
{
    public OfficesByNumber(string number)
    {
        Predicate = office => office.RegistryPhoneNumber!.StartsWith(number);
    }
}