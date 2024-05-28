using System.Linq.Expressions;
using System.Reflection;
using Application.Abstractions.Persistence.Repositories.Specification;

namespace Application.Services.Specification;

public class SpecificationExpander : ExpressionVisitor
{
    protected override Expression VisitUnary(UnaryExpression node)
    {
        if (node.NodeType != ExpressionType.Convert)
        {
            return base.VisitUnary(node);
        }

        var method = node.Method;

        if (method == null || method.Name != "op_Implicit")
        {
            return base.VisitUnary(node);
        }

        var declaringType = method.DeclaringType;

        if (!declaringType!.GetTypeInfo().IsGenericType
            || declaringType!.GetGenericTypeDefinition() != typeof(BaseSpecification<>))
        {
            return base.VisitUnary(node);
        }

        const string name = nameof(BaseSpecification<object>.ToExpression);

        var toExpression = declaringType.GetMethod(name);

        return ExpandSpecification(node.Operand, toExpression!);

    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        var method = node.Method;

        if (method.Name != nameof(IBaseSpecification<object>.ToExpression))
        {
            return base.VisitMethodCall(node);
        }

        var declaringType = method.DeclaringType;
        var interfaces = declaringType!.GetTypeInfo().GetInterfaces();

        return interfaces.Any(i => i.GetTypeInfo().IsGenericType
            && i.GetGenericTypeDefinition() == typeof(IBaseSpecification<>)) 
            ? ExpandSpecification(node.Object!, method) : base.VisitMethodCall(node);
    }

    private Expression ExpandSpecification(Expression instance, MethodInfo toExpression)
    {
        return Visit((Expression)GetValue(Expression.Call(instance, toExpression)));
    }

    // http://stackoverflow.com/a/2616980/1402923
    private static object GetValue(Expression expression)
    {
        var objectMember = Expression.Convert(expression, typeof(object));
        var getterLambda = Expression.Lambda<Func<object>>(objectMember);
        return getterLambda.Compile().Invoke();
    }
}