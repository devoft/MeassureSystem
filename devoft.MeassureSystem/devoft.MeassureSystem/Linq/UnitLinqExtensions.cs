using System.Linq;
using System.Runtime.CompilerServices;

namespace devoft.MeassureSystem.Linq
{
    public static class UnitLinqExtensions
    {
        public static IQueryable<T> AllowUnits<T>(this IQueryable<T> query)
            => new UnitsQueryable<T>(query);
    }
}
