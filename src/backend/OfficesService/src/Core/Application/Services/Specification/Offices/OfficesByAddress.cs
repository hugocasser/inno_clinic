using Domain.Models;

namespace Application.Services.Specification.Offices;

public class OfficesByAddress : BaseSpecification<Office>
{
    public OfficesByAddress(string address)
    {
        Predicate = office => office.Address!.StartsWith(address);
    }
}