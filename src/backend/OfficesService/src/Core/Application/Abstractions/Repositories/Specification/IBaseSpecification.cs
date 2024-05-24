using System.Linq.Expressions;
using Domain.Abstractions;

namespace Application.Abstractions.Repositories.Specification;

public interface IBaseSpecification<T> 
{
    public bool IsSatisfiedBy(T entity);
    public Expression<Func<T, bool>> ToExpression();
}