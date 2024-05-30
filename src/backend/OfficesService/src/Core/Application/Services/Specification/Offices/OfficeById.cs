using Domain.Models;

namespace Application.Services.Specification.Offices;

public class OfficeById : BaseSpecification<Office>
{
    public OfficeById(Guid id)
    {
        Predicate = office => office.Id == id;
    }
}