using Domain.Models;

namespace Application.Services.Specification.Offices;

public class InactiveOffices : BaseSpecification<Office>
{
    public InactiveOffices()
    {
        Predicate = office => !office.IsActive;
    }
}