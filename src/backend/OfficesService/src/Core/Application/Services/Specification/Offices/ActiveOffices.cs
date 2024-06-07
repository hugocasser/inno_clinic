using Domain.Models;

namespace Application.Services.Specification.Offices;

public class ActiveOffices : BaseSpecification<Office>
{
    public ActiveOffices()
    {
        Predicate = office => office.IsActive;
    }
}