namespace UnitTest
{
    public static class ListExtension
    {
        public static bool ListEquals<T>(this List<T> list1, List<T> list2, IEqualityComparer<T>? compareEquality = default)
        {
            if (compareEquality == default)
            {
                return !list2.Except(list1).Any() && !list1.Except(list2).Any();
            }
            else
            {
                return !list2.Except(list1, compareEquality).Any() && !list1.Except(list2, compareEquality).Any();
            }
        }
        public static bool ListEquals<T>(this List<T> list1, IQueryable<T> list2, IEqualityComparer<T> compareEquality)
        => !list2.Except(list1, compareEquality).Any() && !list1.Except(list2, compareEquality).Any();
    }

    /// <summary>
    /// https://www.cnblogs.com/ldp615/archive/2011/08/02/quickly-create-instance-of-iequalitycomparer-and-icomparer.html
    /// 快速建立IEqualityComparer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Equality<T>
    {

        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector)
        {
            return new CommonEqualityComparer<V>(keySelector);
        }
        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return new CommonEqualityComparer<V>(keySelector, comparer);
        }

        class CommonEqualityComparer<V> : IEqualityComparer<T>
        {
            private Func<T, V> keySelector;
            private IEqualityComparer<V> comparer;

            public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
            {
                this.keySelector = keySelector;
                this.comparer = comparer;
            }
            public CommonEqualityComparer(Func<T, V> keySelector)
                : this(keySelector, EqualityComparer<V>.Default)
            { }

            public bool Equals(T x, T y)
            {
                return comparer.Equals(keySelector(x), keySelector(y));
            }
            public int GetHashCode(T obj)
            {
                return comparer.GetHashCode(keySelector(obj));
            }
        }
    }
}