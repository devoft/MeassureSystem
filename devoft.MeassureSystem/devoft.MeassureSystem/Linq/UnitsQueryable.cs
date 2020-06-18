using devoft.System.Linq;
using System.Linq;

namespace devoft.MeassureSystem.Linq
{
    public class UnitsQueryable<T> : QueryableAdapter<T>
    {
        public UnitsQueryable(IQueryable<T> query) : base(query)
        {
        }

        protected override IQueryProvider BuildQueryProviderOverride(IQueryProvider wrapped)
            => new UnitsQueryProvider<T>(wrapped);
    }
}
