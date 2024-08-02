namespace DataShareHub.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> Except<TSource, VSource>(this IEnumerable<TSource> first, IEnumerable<VSource> second, Func<TSource, VSource, bool> comparer)
        {
            return first.Where(x => second.Count(y => comparer(x, y)) == 0);
        }

        public static IEnumerable<TSource> Contains<TSource, VSource>(this IEnumerable<TSource> first, IEnumerable<VSource> second, Func<TSource, VSource, bool> comparer)
        {
            return first.Where(x => second.FirstOrDefault(y => comparer(x, y)) != null);
        }

        public static IEnumerable<TSource> Intersect<TSource, VSource>(this IEnumerable<TSource> first, IEnumerable<VSource> second, Func<TSource, VSource, bool> comparer)
        {
            return first.Where(x => second.Count(y => comparer(x, y)) == 1);
        }
    }
}
