using devoft.System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace devoft.MeassureSystem.Linq
{
    internal class UnitsExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
            => node is { Object: { Type: var type } } && IsUnit(type) || IsUnit(node.Type)
                ? Visit(Expression.Constant(Normalize(node)))
                : base.VisitMethodCall(node);

        protected override Expression VisitIndex(IndexExpression node)
            => IsUnit(node.Type)
                ? Visit(Expression.Constant(Normalize(node)))
                : base.VisitIndex(node);

        protected override Expression VisitMember(MemberExpression node)
            => node is { Expression: { Type: var type } } && IsUnit(type) || IsUnit(node.Type)
                ? Visit(Expression.Constant(Normalize(node)))
                : base.VisitMember(node);

        protected override Expression VisitUnary(UnaryExpression node)
            => node is {
                           NodeType: ExpressionType.Convert,
                           Operand: { Type: var type },
                           Method: MethodInfo method
                       }
                ? IsUnit(type)
                    ? method.Name.In("op_Implicit", "op_Explicit")
                        ? Visit(Expression.Constant(Normalize(node)))
                        : base.VisitUnary(node)
                    : IsUnit(node.Type)
                        ? throw new NotSupportedException($"Conversion from type: {type.Name} to: {node.Type.Name} is not allowed in this context")
                        : base.VisitUnary(node)
                : base.VisitUnary(node)
;

        protected override Expression VisitBinary(BinaryExpression node) 
            => IsUnit(node.Left.Type) 
               && IsUnit(node.Right.Type)
               && IsComparissonExp(node.NodeType)
                    ? Visit(Expression.MakeBinary(node.NodeType,
                                                  Visit(Expression.Convert(Visit(node.Left), ScalarTypeForUnit(node.Left.Type))), 
                                                  Visit(Expression.Convert(Visit(node.Right), ScalarTypeForUnit(node.Right.Type)))))
                    : base.VisitBinary(node);

        private object Normalize(Expression node)
            => Expression.Lambda(node).Compile().DynamicInvoke();

        private bool IsComparissonExp(ExpressionType type)
            => type.In(ExpressionType.Equal, 
                       ExpressionType.NotEqual, 
                       ExpressionType.GreaterThan,
                       ExpressionType.LessThan,
                       ExpressionType.GreaterThanOrEqual,
                       ExpressionType.LessThanOrEqual);

        private bool IsUnit(Type type)
            => type.In(typeof(Time), typeof(Length), typeof(Weight), typeof(Pixel), typeof(Area), typeof(Volume));

        private Type ScalarTypeForUnit(Type type)
            => type.Name switch 
               {
                   nameof(Time)    => typeof(TimeSpan),
                   nameof(Length)  => typeof(decimal),
                   nameof(Weight)  => typeof(decimal),
                   nameof(Pixel)   => typeof(int),
                   nameof(Area)    => typeof(decimal),
                   nameof(Volume)  => typeof(decimal),
                   _               => default
               };
    }
}
