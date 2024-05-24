using System.Linq.Expressions;
using Application.Abstractions.Repositories.Specification;
using Domain.Abstractions;

namespace Application.Services.Specification;

public class BaseSpecification<T> : IBaseSpecification<T>
{
    private Func<T, bool> _function;

    private Func<T, bool> Function =>
        _function ??= Predicate.Compile();

    protected Expression<Func<T, bool>> Predicate;

    protected BaseSpecification() { }

    public BaseSpecification(Expression<Func<T, bool>> predicate)
    {
        Predicate = predicate;
    }

    public bool IsSatisfiedBy(T entity)
    {
        return Function.Invoke(entity);
    }

    public Expression<Func<T, bool>> ToExpression()
    {
        return Predicate;
    }
    
    public static implicit operator Func<T, bool>(BaseSpecification<T> spec)
    {
        return spec.Function;
    }

    public static implicit operator Expression<Func<T, bool>>(BaseSpecification<T> spec)
    {
        return spec.Predicate;
    }

    public static bool operator true(BaseSpecification<T> spec)
    {
        return false;
    }

    public static bool operator false(BaseSpecification<T> spec)
    {
        return false;
    }

    public static BaseSpecification<T> operator !(BaseSpecification<T> spec)
    {
        return new BaseSpecification<T>(
            Expression.Lambda<Func<T, bool>>(
                Expression.Not(spec.Predicate.Body),
                spec.Predicate.Parameters));
    }

    public static BaseSpecification<T> operator &(BaseSpecification<T> left, BaseSpecification<T> right)
    {
        var leftExpr = left.Predicate;
        var rightExpr = right.Predicate;
        var leftParam = leftExpr.Parameters[0];
        var rightParam = rightExpr.Parameters[0];

        return new BaseSpecification<T>(
            Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    leftExpr.Body,
                    new ParameterReplacer(rightParam, leftParam).Visit(rightExpr.Body)),
                leftParam));
    }

    public static BaseSpecification<T> operator |(BaseSpecification<T> left, BaseSpecification<T> right)
    {
        var leftExpr = left.Predicate;
        var rightExpr = right.Predicate;
        var leftParam = leftExpr.Parameters[0];
        var rightParam = rightExpr.Parameters[0];


        return new BaseSpecification<T>(
            Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    leftExpr.Body,
                    new ParameterReplacer(rightParam, leftParam).Visit(rightExpr.Body)),
                leftParam));
    }
}