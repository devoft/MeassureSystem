using devoft.Core.Patterns;
using devoft.System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace devoft.MeassureSystem.Linq
{
    public class UnitsQueryProvider<T> : QueryProviderAdapter<UnitsQueryable<T>, T>
    {
        public UnitsQueryProvider(IQueryProvider wrapped) : base(wrapped)
        {
        }

        public override IQueryable<TItem> CreateQuery<TItem>(Expression expression)
            => new UnitsQueryable<TItem>(_wrapped.CreateQuery<TItem>(expression));

        public override TResult Execute<TResult>(Expression expression)
        {
            var visitor = new UnitsExpressionVisitor();
            var normalizedExpression = visitor.Visit(expression);

            return (TResult) (typeof(IQueryable).IsAssignableFrom(normalizedExpression.Type)
                                ? _wrapped.CreateQuery(normalizedExpression)
                                : _wrapped.Execute(normalizedExpression));
        }

    }
}
