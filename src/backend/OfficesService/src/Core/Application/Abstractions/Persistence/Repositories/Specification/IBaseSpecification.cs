using System.Linq.Expressions;

namespace Application.Abstractions.Persistence.Repositories.Specification;

public interface IBaseSpecification<T> 
{
    public bool IsSatisfiedBy(T entity);
    public Expression<Func<T, bool>> ToExpression();
}