namespace ParallelBfs.Sdk
{
    using System;
    using System.Collections.Generic;

    public static class IEnumerableExtentions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> expression)
        {
            foreach (var item in items)
            {
                expression(item);
            }
        }
    }
}
