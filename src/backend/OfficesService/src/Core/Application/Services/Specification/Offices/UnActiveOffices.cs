using Domain.Models;

namespace Application.Services.Specification.Offices;

public class UnActiveOffices : BaseSpecification<Office>
{
    public UnActiveOffices()
    {
        Predicate = office => !office.IsActive;
    }
}