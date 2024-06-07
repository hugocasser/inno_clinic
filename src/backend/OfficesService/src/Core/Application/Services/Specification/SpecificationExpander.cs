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

        if (method == null || method.Name != "op_Implicit" || !IsBaseSpecificationConversion(method))
        {
            return base.VisitUnary(node);
        }

        var toExpression = GetToExpressionMethod(method.DeclaringType!);

        return ExpandSpecification(node.Operand, toExpression!);
    }

    private static bool IsBaseSpecificationConversion(MemberInfo method)
    {
        var declaringType = method.DeclaringType;
        return declaringType != null &&
            declaringType.GetTypeInfo().IsGenericType &&
            declaringType.GetGenericTypeDefinition() == typeof(BaseSpecification<>);
    }

    private static MethodInfo GetToExpressionMethod(Type declaringType)
    {
        const string name = nameof(BaseSpecification<object>.ToExpression);
        return declaringType.GetMethod(name);
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