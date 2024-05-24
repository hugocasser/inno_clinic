using System.Linq.Expressions;
using Application.Abstractions.Repositories.Specification;
using Domain.Models;

namespace Application.Services.Specification.Offices;

public class OfficeByAddress : BaseSpecification<Office>
{
    public OfficeByAddress(string address)
    {
        Predicate = office => office.Address == address;
    }
}