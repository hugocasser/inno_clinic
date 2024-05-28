using System.Linq.Expressions;
using ExpressionVisitor = MongoDB.Bson.Serialization.ExpressionVisitor;

namespace Application.Services.Specification;

internal class ParameterReplacer(ParameterExpression parameter, ParameterExpression replacement)
    : ExpressionVisitor
{
    protected override Expression VisitParameter(ParameterExpression node)
    {
        return base.VisitParameter(parameter == node ? replacement : node);
    }

    public new Expression Visit(Expression node)
    {
        return base.Visit(node);
    }
}