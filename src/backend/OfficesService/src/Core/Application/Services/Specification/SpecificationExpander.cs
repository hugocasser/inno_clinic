using System.Linq.Expressions;
using System.Reflection;
using Application.Abstractions.Repositories.Specification;

namespace Application.Services.Specification;

public class SpecificationExpander : ExpressionVisitor
{
    protected override Expression VisitUnary(UnaryExpression node)
    {
        if (node.NodeType == ExpressionType.Convert)
        {
            MethodInfo method = node.Method;

            if (method != null && method.Name == "op_Implicit")
            {
                Type declaringType = method.DeclaringType;

                if (declaringType.GetTypeInfo().IsGenericType
                    && declaringType.GetGenericTypeDefinition() == typeof(BaseSpecification<>))
                {
                    const string name = nameof(BaseSpecification<object>.ToExpression);

                    MethodInfo toExpression = declaringType.GetMethod(name);

                    return ExpandSpecification(node.Operand, toExpression);
                }
            }
        }

        return base.VisitUnary(node);
    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        MethodInfo method = node.Method;

        if (method.Name == nameof(IBaseSpecification<object>.ToExpression))
        {
            Type declaringType = method.DeclaringType;
            Type[] interfaces = declaringType.GetTypeInfo().GetInterfaces();

            if (interfaces.Any(i => i.GetTypeInfo().IsGenericType
                && i.GetGenericTypeDefinition() == typeof(IBaseSpecification<>)))
            {
                return ExpandSpecification(node.Object, method);
            }
        }

        return base.VisitMethodCall(node);
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