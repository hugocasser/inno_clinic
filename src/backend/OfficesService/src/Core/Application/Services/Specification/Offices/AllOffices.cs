using Domain.Models;

namespace Application.Services.Specification.Offices;

public class AllOffices : BaseSpecification<Office>
{
    public AllOffices()
    {
        Predicate = office => true;
    }
}