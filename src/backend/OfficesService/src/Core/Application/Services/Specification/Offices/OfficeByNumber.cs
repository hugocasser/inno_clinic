using Domain.Models;

namespace Application.Services.Specification.Offices;

public class OfficeByNumber : BaseSpecification<Office>
{
    public OfficeByNumber(string number)
    {
        Predicate = office => office.Address == number;
    }
}